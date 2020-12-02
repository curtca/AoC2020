using System;

namespace lib11
{
    public class Reports
    {
        public int ProductOf2020Sum(int [] expenses)
        {
            for (int i = 1; i < expenses.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (expenses[i] + expenses[j] == 2020)
                        return expenses[i] * expenses[j];
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
