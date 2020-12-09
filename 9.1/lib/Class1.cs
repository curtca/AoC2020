using System;
using System.Collections.Generic;
using System.Linq;

public class XMAS
{
    long[] nums;
    public XMAS(string input)
    {
        var lines = input.Split("\r\n");
        nums = new long[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            nums[i] = long.Parse(lines[i]);
        }
    }

    public long FindInvalid(int preamble)
    {
        for (int i = preamble; i < nums.Length; i++) // candidate invalid num
        {
            bool valid = false;
            for (int j = i - preamble; j < i - 1; j++)
            {
                for (int k = j + 1; k < i; k++)
                {
                    if (nums[j] + nums[k] == nums[i])
                        valid = true;
                }
            }
            if (!valid)
                return nums[i];
        }

        return -1;
    }
}
