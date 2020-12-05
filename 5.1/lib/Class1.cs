using System;
using System.Collections.Generic;

public class Seats
{
    string[] lines;
    public Seats(string input)
    {
        lines = input.Split("\r\n");
    }

    public long MaxID()
    {
        long max = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            long id = ID(lines[i]);
            if (id > max)
                max = id;
        }

        return max;
    }

    private long ID(string v)
    {
        int maxrow = 127;
        int minrow = 0;
        int maxseat = 7;
        int minseat = 0;

        for (int i = 0; i < 7; i++)
        {
            int shrink = 1 << (6 - i);
            if (v[i] == 'F')
                maxrow -= shrink;
            else
                minrow += shrink;
        }
        for (int i = 0; i < 3; i++)
        {
            int shrink = 1 << (2 - i);
            if (v[7 + i] == 'L')
                maxseat -= shrink;
            else
                minseat += shrink;
        }
        return minrow * 8 + minseat;
    }
}
