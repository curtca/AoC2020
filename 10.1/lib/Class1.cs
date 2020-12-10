using System;
using System.Collections.Generic;
using System.Linq;

public class Adapters
{
    long[] nums;
    public Adapters(string input)
    {
        var lines = input.Split("\r\n");
        nums = new long[lines.Length];
        for (int i = 0; i < lines.Length; i++)
            nums[i] = long.Parse(lines[i]);
    }

    public long Diffs1x3()
    {
        var sorted = nums.OrderBy(n => n).ToArray();
        long[] diffs = new long[4];
        long last = 0;
        for (int i = 0; i < sorted.Length; i++)
        {
            long diff = sorted[i] - last;
            last = sorted[i];
            diffs[diff]++;
        }

        return diffs[1] * (diffs[3] + 1);
    }

}
