using System;

namespace lib12
{
    public class Reports
    {
        public int ProductOf2020SumOf3(int[] expenses)
        {
            for (int i = 2; i < expenses.Length; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    for (int k = 0; k < j; k++)
                    {
                        if (expenses[i] + expenses[j] + +expenses[k] == 2020)
                            return expenses[i] * expenses[j] * expenses[k];
                    }
                }
            }
            return 0;
        }

        public int[] ParseInput(string input)
        {
            string[] lines = input.Split('\n');
            var expenses = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                expenses[i] = int.Parse(lines[i]);
            }
            return expenses;
        }
    }
}
