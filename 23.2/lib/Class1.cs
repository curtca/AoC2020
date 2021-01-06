using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Cup
{
    public Cup Next;
    public int Value;
    public Cup(int v)    {Value = v;}
}

public class Cups
{
    Cup current = null;
    int len = 0;
    int loops = 0;
    Cup[] cups = null;
    
    public Cups(string input, int len, int loops)
    {
        this.len = len;
        this.loops = loops;
        Cup last = null;
        Cup prev = last;
        cups = new Cup[len + 1]; // never using index 0
        for (int n = len; n > input.Length; n--)
        {
            Cup c = new Cup(n);
            if (last == null) last = c;
            c.Next = prev;
            cups[n] = c;
            prev = c;
        }

        foreach(int n in input.Select(c => c - '0').Reverse())
        {
            Cup c = new Cup(n);
            if (last == null) last = c;
            c.Next = prev;
            cups[n] = c;
            prev = c;
        }

        current = prev;
        last.Next = prev; // circular!
    }

    public long OneHundred()
    {
        // "curent" could be anywhere!
        for (int loop = 0; loop < loops; loop++)
        {
            int dest = (current.Value + len - 2) % len + 1; // value
            while (dest == current.Next.Value 
                || dest == current.Next.Next.Value
                || dest == current.Next.Next.Next.Value)
                dest = (dest + len - 2) % len + 1;
            var taken = current.Next;
            current.Next = taken.Next.Next.Next;
            Cup destCup = cups[dest];
            taken.Next.Next.Next = destCup.Next;
            destCup.Next = taken;
            current = current.Next;

            if (loop % 10_000 == 0)
                Debug.Print(loop.ToString());
        }

        Cup cup = cups[1];
        return (long) cup.Next.Value * cup.Next.Next.Value;
    }


}
