using System;
using System.Collections.Generic;
using System.Linq;

public class Bags
{
    Dictionary<string, List<Tuple<int, string>>> bagContains = new Dictionary<string, List<Tuple<int, string>>>(); // key contains can be held by value (set);
    public Bags(string input)
    {
        var bags = input.Split("\r\n");
        for (int i = 0; i < bags.Length; i++)
        {
            string[] twobags = bags[i].Split(" bags contain ");
            string outerBag = twobags[0];
            string[] innerBags = twobags[1].Split(", ");
            bagContains[outerBag] = new List<Tuple<int, string>>();
            for (int j = 0; j < innerBags.Length; j++)
            {
                string[] parts = innerBags[j].Split(" "); // e.g. "2 shiny gold bags"
                string innerBag = parts[1] + ' ' + parts[2]; 
                if (parts[1] != "other") // from "no other bags"
                {
                    bagContains[outerBag].Add(new Tuple<int, string>(int.Parse(parts[0]), innerBag));
                }
            }
        }
    }

    public int TotalBags(string type)
    {
        int count = 0;
        Queue<Tuple<int, string>> toProcess = new Queue<Tuple<int, string>>();
        toProcess.Enqueue(new Tuple<int, string>(1, type));
        while (toProcess.Count > 0)
        {
            var bag1 = toProcess.Dequeue();
            int qty1 = bag1.Item1;
            string type1 = bag1.Item2;
            foreach (var bag2 in bagContains[type1])
            {
                int qty2 = bag2.Item1;
                string type2 = bag2.Item2;
                int qty = qty1 * qty2;
                count += qty;
                toProcess.Enqueue(new Tuple<int, string>(qty, type2));
            }
        }

        return count;
    }

}
