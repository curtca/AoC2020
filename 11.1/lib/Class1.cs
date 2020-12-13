using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Seats
{
    char[,] seats; // x,y
    int dx = 0, dy = 0;
    public Seats(string input)
    {
        var lines = input.Split("\r\n");
        seats = new char[lines[0].Length, lines.Length];
        dx = lines[0].Length;
        dy = lines.Length;
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                seats[x, y] = lines[y][x];
    }

    public long OccupiedSeatsInStatis()
    {
        int round = 0;
        do
        {
            Debug.Print($"After Round {round}:\n{DumpSeats()}\n\n");
            round++;
        } while (AdvanceRound());
        Debug.Print($"After Round {round}:\n{DumpSeats()}\n\n");

        int occupied = 0;
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                if (seats[x, y] == '#')
                    occupied++;

        return occupied;
    }

    private string DumpSeats()
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < dy; y++)
        {
            for (int x = 0; x < dx; x++)
            {
                sb.Append(seats[x, y]);
            }
            sb.Append('\n');
        }
        return sb.ToString();
    }

    private bool AdvanceRound()
    {
        bool changed = false;
        char[,] next = new char[dx, dy];
        for (int y = 0; y < dy; y++)
        {
            for (int x = 0; x < dx; x++)
            {
                int on = OccupiedNeighbors(x, y);
                // If a seat is empty(L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                if (seats[x, y] == 'L' && on == 0)
                {
                    next[x, y] = '#';
                    changed = true;
                }
                // If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                else if (seats[x, y] == '#' && on >= 4)
                {
                    next[x, y] = 'L';
                    changed = true;
                }
                // Otherwise, the seat's state does not change.
                else
                {
                    next[x, y] = seats[x, y];
                }
            }
        }
        seats = next;

        return changed;
    }

    private int OccupiedNeighbors(int x0, int y0)
    {
        int on = 0;
        for (int y = Math.Max(0, y0 - 1); y <= Math.Min(dy - 1, y0 + 1); y++)
        {
            for (int x = Math.Max(0, x0 - 1); x <= Math.Min(dx - 1, x0 + 1); x++)
            {
                if (!(x == x0  && y == y0) && seats[x, y] == '#')
                    on++;
            }
        }
        return on;
    }
}