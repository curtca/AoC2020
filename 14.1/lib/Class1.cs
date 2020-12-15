using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class Mask
{
    string[] cmds;
    public Mask(string input)
    {
        cmds = input.Split("\r\n");
    }

    public ulong Sum()
    {
        string mask = "";
        Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();
        for (int i = 0; i < cmds.Length; i++)
        {
            var parts = cmds[i].Split(" = ");
            string left = parts[0];
            if (left == "mask")
            {
                mask = parts[1];
            }
            else // memory assignment
            {
                ulong loc = ulong.Parse(left.Substring(4, left.Length - 5));
                ulong val = ulong.Parse(parts[1]);
                ApplyMask(ref val, mask);
                memory[loc] = val;
            }
        }

        ulong sum = memory.Aggregate(0UL, (a, mem) => a + mem.Value);
        return sum;
    }

    private void ApplyMask(ref ulong val, string mask)
    {
        for (int i = 0; i < 36; i++)
        {
            switch (mask[35 - i])
            {
                case 'X': // no op
                    break;

                case '1':
                    val |= (1UL) << i;
                    break;

                case '0':
                    val &= ~(1UL << i);
                    break;

            }
        }
    }
}