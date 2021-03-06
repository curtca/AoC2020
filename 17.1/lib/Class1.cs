﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Conway
{
    char[,] init;
    public Conway(string input)
    {
        string[] lines = input.Split("\r\n");
        init = new char[lines[0].Length, lines.Length];
        Debug.Assert(lines[0].Length == lines.Length, "Assuming square input.");
        for(int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
                init[x, y] = lines[y][x];
        }
    }

    public long RunCycles(int cycles)
    {
        int offset = cycles; // might matter later
        int inputsize = init.GetLength(0);
        int size = offset * 2 + inputsize; // square input only

        // Map input to 3d space
        bool[,,,] space = new bool[size, size, size, size]; // gratuitous large in z & w, but whatever
        for (int x = 0; x < inputsize; x++)
            for (int y = 0; y < inputsize; y++)
                space[x + offset, y + offset, offset, offset] = init[x, y] == '#';

        for (int cycle = 0; cycle < cycles; cycle++)
            RunCycle(ref space);

        int actives = 0;
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                for (int z = 0; z < size; z++)
                    for (int w = 0; w < size; w++)
                        if (space[x, y, z, w])
                        actives++;

        return actives;
    }

    private void RunCycle(ref bool[,,,] space)
    {
        int size = space.GetLength(0);
        bool[,,,] nextspace = new bool[size, size, size, size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    for (int w = 0; w < size; w++)
                    {
                        int activeneighbors = ActiveNeighbors(space, x, y, z, w);
                        if (space[x, y, z, w])
                        {
                            nextspace[x, y, z, w] = activeneighbors == 2 || activeneighbors == 3;
                        }
                        else
                        {
                            nextspace[x, y, z, w] = activeneighbors == 3;
                        }
                    }
                }
            }
        }
        space = nextspace;
    }

    private int ActiveNeighbors(bool[,,,] space, int x, int y, int z, int w)
    {
        int size = space.GetLength(0);
        int activeneighbors = 0;
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                for (int dz = -1; dz <= 1; dz++)
                    for (int dw = -1; dw <= 1; dw++)
                    {
                        int x2 = x + dx, y2 = y + dy, z2 = z + dz, w2 = w + dw;
                    if (x2 >= 0 && x2 < size && y2 >= 0 && y2 < size && z2 >= 0 && z2 < size && w2 >= 0 && w2 < size
                        && !(x2 == x && y2 == y && z2 == z && w2 == w) && space[x2, y2, z2, w2])
                        activeneighbors++;
                }

        return activeneighbors;
    }
}