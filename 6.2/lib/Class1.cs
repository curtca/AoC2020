using System;
using System.Collections.Generic;
using System.Linq;

public class Customs
{
    string[] forms;
    public Customs(string input)
    {
        forms = input.Split("\r\n\r\n");
    }

    public long SumOfQsEveryoneAnswered()
    {
        long sum = 0;
        long[] ids = new long[forms.Length];
        for (int i = 0; i < forms.Length; i++)
        {
            int clines = forms[i].Split("\r\n").Length;
            string form = forms[i].Replace("\r\n", null);
            var chars = new Dictionary<char, int>();
            for (int j = 0; j < form.Length; j++)
            {
                char c = form[j];
                if (chars.ContainsKey(c))
                    chars[c] = chars[c] + 1;
                else
                    chars.Add(c, 1);
            }

            var allanswered = from common in chars.Values
                              where common == clines
                              select 1;
            sum += allanswered.Count();
        }

        return sum;
    }
}
