using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Map
{
    string[] lines;
    public Map(string input)
    {
        lines = input.Split("\r\n");
    }

    public long ManhattanDistance()
    {
        int x = 0, y = 0, wx = 10, wy = 1;

        for (int i = 0; i < lines.Length; i++)
        {
            Move(ref x, ref y, ref wx, ref wy, lines[i]);
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    private void Move(ref int x, ref int y, ref int wx, ref int wy, string cmd)
    {
        char op = cmd[0];
        int n = int.Parse(cmd.Substring(1));
        switch (op)
        {
            case 'N':
                wy += n;
                break;
            case 'S':
                wy -= n;
                break;
            case 'E':
                wx += n;
                break;
            case 'W':
                wx -= n;
                break;
            case 'L':
                Clockwise(ref wx, ref wy, ((360-n) / 90) % 4);
                break;
            case 'R':
                Clockwise(ref wx, ref wy, (n / 90) % 4);
                break;
            case 'F':
                x += wx * n;
                y += wy * n;
                break;
            default:
                throw new Exception($"Unexpected command op code: {op}");
        }
    }

    private void Clockwise(ref int wx, ref int wy, int rots)
    {
        int wxT = wx, wyT = wy;
        for (int i = 0; i < rots; i++)
        {
            wxT = wy;
            wyT = -wx;
            wx = wxT;
            wy = wyT;
        }
    }
}