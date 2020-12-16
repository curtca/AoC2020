using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Memory
{
    Dictionary<long, long> nums = new Dictionary<long, long>(); // k = num spoken, v = when last spoken
    public Memory(string input)
    {
        var strnums = input.Split(",");
        foreach (long n in (from sn in strnums select long.Parse(sn)))
            nums.Add(n, nums.Count); // assuming no dupes in input
    }

    public long Nth(int n)
    {
        long next = 0; // cheating because we know first next is 0 for all inputs
        long last = 0; // last number spoken
        for (int i = nums.Count; i < 30_000_000; i++)
        {
            last = next;
            long iprev = 0;
            bool found = nums.TryGetValue(next, out iprev);

            nums[next] = i;
            next = found ? i - iprev : 0;
        }
        return last;
    }
}