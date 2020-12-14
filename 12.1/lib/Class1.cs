using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Map
{
    string[] lines;
    (int, int)[] dirs = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
    public Map(string input)
    {
        lines = input.Split("\r\n");
    }

    public long ManhattanDistance()
    {
        int x = 0, y = 0;
        int f = 1; // 0 = north, 1 = east, 2 = south, 3 = west

        for (int i = 0; i < lines.Length; i++)
        {
            Move(ref x, ref y, ref f, lines[i]);
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    private void Move(ref int x, ref int y, ref int f, string cmd)
    {
        char op = cmd[0];
        int n = int.Parse(cmd.Substring(1));
        switch (op)
        {
            case 'N':
                y += n;
                break;
            case 'S':
                y -= n;
                break;
            case 'E':
                x += n;
                break;
            case 'W':
                x -= n;
                break;
            case 'L':
                f = (f + 4 - n / 90) % 4; // don't use mod on negative #s
                break;
            case 'R':
                f = (f + n / 90) % 4;
                break;
            case 'F':
                x += dirs[f].Item1 * n;
                y += dirs[f].Item2 * n;
                break;
            default:
                throw new Exception($"Unexpected command op code: {op}");
        }
    }
}