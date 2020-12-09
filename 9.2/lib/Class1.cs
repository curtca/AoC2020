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

    public long FindWeakness(int preamble)
    {
        long invalid = FindInvalid(preamble);
        int i = 0, j = 1;
        while (j < nums.Length)
        {
            long sum = 0;
            for (int k = i; k <= j; k++)
                sum += nums[k];

            if (sum == invalid)
            {
                long min = long.MaxValue, max = long.MinValue;
                for (int k = i; k <= j; k++)
                {
                    if (min > nums[k]) min = nums[k];
                    if (max < nums[k]) max = nums[k];
                }
                return min + max;
            }

            else if (sum > invalid)
            {
                i++;
                j = i + 1;
            }
            else
                j++;
        }
        return -1;
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
