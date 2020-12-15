using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Bus
{
    string[] buses;
    (int, long)[] targetMods; // Item1 = bus, Item2 = target mod at time T
    public Bus(string input)
    {
        var lines = input.Split("\r\n");
        buses = lines[1].Split(',');
        var mods = new (int, long)[buses.Length];
        for (int i = 0; i < buses.Length; i++)
        {
            if (buses[i] == "x") continue;
            int s = int.Parse(buses[i]);
            mods[i].Item1 = s;
            mods[i].Item2= (s * i - i) % s;
        }
        targetMods = (from (int, long) tuple in mods
                      where tuple.Item1 > 0
                      // orderby -tuple.Item1
                      select tuple).ToArray();
    }

    public long Sync()
    {
        // Solve "bus by bus". Increment T by 1st bus schedule until 2nd bus has right mod. 
        // Then multiply and advance by that until next bus has right mod. Rinse and repeat.
        long t = 0;
        long inc = 1;
        for (int i = 0; i < targetMods.Length; i++)
        {
            while (t % targetMods[i].Item1 != targetMods[i].Item2)
                t += inc;

            inc *= targetMods[i].Item1;
        }
        return t;
    }
}