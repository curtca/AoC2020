using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Bus
{
    long earliest;
    string[] buses; 
    public Bus(string input)
    {
        var lines = input.Split("\r\n");
        earliest = long.Parse(lines[0]);
        buses = lines[1].Split(',');
    }

    public long Earliest()
    {
        long wait = long.MaxValue;
        long id = 0;
        for (int i = 0; i < buses.Length; i++)
        {
            long sched;
            if (long.TryParse(buses[i], out sched))
            {
                long waitT = (sched - earliest % sched) % sched;
                if (waitT < wait)
                {
                    wait = waitT;
                    id = sched;
                }
            }
        }

        return id * wait;
    }
}