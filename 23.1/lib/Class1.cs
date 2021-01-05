using System;
using System.Collections.Generic;
using System.Linq;

public class Cups
{
    List<int>cups = null;
    int len = 0;
    public Cups(string input)
    {
        cups = input.Select(c => c - '0').ToList();
        len = cups.Count;
    }

    public string OneHundred()
    {
        // "curent" cup is ALWAYS at front
        for (int loops = 0; loops < 100; loops++)
        {
            int dest = (cups[0] + len - 2) % len + 1; // value
            while (dest == cups[1] || dest == cups[2] || dest == cups[3])
                dest = (dest + len - 2) % len + 1;
            var taken = cups.GetRange(1, 3);
            cups.RemoveRange(1, 3);
            int idest = cups.IndexOf(dest);
            cups.InsertRange(idest + 1, taken);
            cups.Add(cups[0]);
            cups.RemoveAt(0);
        }
        var i1 = cups.IndexOf(1);
        var from1 = cups.GetRange(i1 + 1, len - i1 - 1);
        from1.AddRange(cups.GetRange(0, i1));
        return from1.Aggregate("", (a,c) => a+=c);
    }


}
