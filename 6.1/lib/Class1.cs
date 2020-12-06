using System;
using System.Collections.Generic;

public class Customs
{
    string[] forms;
    public Customs(string input)
    {
        forms = input.Split("\r\n\r\n");
    }

    public long SumOfQsAnswered()
    {
        long sum = 0;
        long[] ids = new long[forms.Length];
        for (int i = 0; i < forms.Length; i++)
        {
            string form = forms[i].Replace("\r\n", null);
            var chars = new HashSet<char>();
            for (int j = 0; j < form.Length; j++)
                chars.Add(form[j]);

            sum += chars.Count;
        }

        return sum;
    }
}
