using System;
using System.Collections.Generic;
using System.Linq;

public class Bags
{
    Dictionary<string, HashSet<string>> bagsContain = new Dictionary<string, HashSet<string>>(); // key can be held by value (set);
    public Bags(string input)
    {
        var bags = input.Split("\r\n");
        for (int i = 0; i < bags.Length; i++)
        {
            string[] twobags = bags[i].Split(" bags contain ");
            string outerBag = twobags[0];
            string[] innerBags = twobags[1].Split(", ");
            for (int j = 0; j < innerBags.Length; j++)
            {
                string[] parts = innerBags[j].Split(" "); // e.g. "2 shiny gold bags"
                string innerBag = parts[1] + ' ' + parts[2]; // possibly "other bags" from "no other bags"
                if (innerBag != "other bags")
                {
                    if (!bagsContain.ContainsKey(innerBag))
                        bagsContain[innerBag] = new HashSet<string>();
                    bagsContain[innerBag].Add(outerBag);
                }
            }
        }
    }

    public int CanHold(string type)
    {
        HashSet<string> allOuter = new HashSet<string>();
        Queue<string> toProcess = new Queue<string>();
        toProcess.Enqueue(type);
        while (toProcess.Count > 0)
        {
            string holder = toProcess.Dequeue();
            if (!bagsContain.ContainsKey(holder))
                continue;
            foreach (string holder2 in bagsContain[holder])
            {
                if (!allOuter.Contains(holder2))
                {
                    allOuter.Add(holder2);
                    toProcess.Enqueue(holder2);
                }
            }
        }

        return allOuter.Count;
    }

}
