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
                mask = parts[1];
            else // memory assignment
            {
                ulong loc = ulong.Parse(left.Substring(4, left.Length - 5));
                ulong val = ulong.Parse(parts[1]);
                ulong[] addrs = ApplyMask(loc, mask);
                for (int iaddr = 0; iaddr < addrs.Length; iaddr++)
                    memory[addrs[iaddr]] = val;
            }
        }

        ulong sum = memory.Aggregate(0UL, (a, mem) => a + mem.Value);
        return sum;
    }

    private ulong[] ApplyMask(ulong loc, string mask)
    {
        int xes = mask.Count(c => c == 'X');
        ulong[] addrs = new ulong[1 << xes];
        for (int iaddr = 0; iaddr < addrs.Length; iaddr++)
        {
            ulong addr = 0;
            int nthx = 0;
            for (int i = 0; i < 36; i++)
            {
                switch (mask[35 - i])
                {
                    case 'X': // 
                        addr |= ((ulong) iaddr & (1UL << nthx)) << (i - nthx);
                        nthx++;
                        break;
                    case '1':
                        addr |= 1UL << i;
                        break;
                    case '0':
                        addr |= (loc & (1UL << i));
                        break;
                }
            }
            addrs[iaddr] = addr;
        }
        return addrs;
    }
}