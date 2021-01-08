using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

enum dir { NONE, E, SE, SW, W, NW, NE }

public class Floor
{
    Dictionary<dir, (int, int)> hexdirs = new Dictionary<dir, (int, int)>(); // q, r
    public Dictionary<(int, int), bool> floor = new Dictionary<(int, int), bool>();
    List<dir>[] tiles;
    public Floor(string input)
    {
        var lines = input.Split("\r\n");
        tiles = new List<dir>[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            tiles[i] = new List<dir>();
            int j = 0;
            string line = lines[i];
            while (j < line.Length)
            {
                dir d = dir.NONE;
                if (line.StartsWith("se"))
                {
                    d = dir.SE;
                    line = line.Substring(2);
                }
                else if (line.StartsWith("ne"))
                {
                    d = dir.NE;
                    line = line.Substring(2);
                }
                else if (line.StartsWith("sw"))
                {
                    d = dir.SW;
                    line = line.Substring(2);
                }
                else if (line.StartsWith("nw"))
                {
                    d = dir.NW;
                    line = line.Substring(2);
                }
                else if (line.StartsWith("e"))
                {
                    d = dir.E;
                    line = line.Substring(1);
                }
                else if (line.StartsWith("w"))
                {
                    d = dir.W;
                    line = line.Substring(1);
                }
                tiles[i].Add(d);
            }
        }

        hexdirs[dir.E] = (1, 0);
        hexdirs[dir.SE] = (0, 1);
        hexdirs[dir.SW] = (-1, 1);
        hexdirs[dir.W] = (-1, 0);
        hexdirs[dir.NW] = (0, -1);
        hexdirs[dir.NE] = (1, -1);
    }

    public int Black()
    {
        foreach (var tile in tiles)
        {
            int q = 0, r = 0;
            foreach (var step in tile)
            {
                (var q1, var r1) = hexdirs[step];
                q += q1; r += r1;
            }
            bool black = false;
            floor.TryGetValue((q, r), out black);
            floor[(q, r)] = !black;
        }

        return floor.Sum(t => t.Value ? 1 : 0);
    }
}
