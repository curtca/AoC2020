using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Memory
{
    List<long> nums = null;
    public Memory(string input)
    {
        var strnums = input.Split(",");
        nums = (from sn in strnums
                    select long.Parse(sn)).ToList();
    }

    public long Nth(int n)
    {
        while (nums.Count < 2020)
        {
            int c = nums.Count;
            int iprev = c - 2;
            while (iprev >= 0 && nums[iprev] != nums[c - 1])
                iprev--;

            if (iprev == -1) // first in list
                nums.Add(0);
            else
                nums.Add(c - iprev - 1);
        }
        return nums.Last();
    }
}