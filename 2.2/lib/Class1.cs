using System;

namespace lib
{
    public class Passwords
    {
        public int CountValid(string input)
        {
            int countValid = 0;
            string[] lines = input.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (IsValid(lines[i]))
                    countValid++;
            }
            return countValid;
        }


        public bool IsValid(string passwordDefn)
        {
            // password includes leading policy, e.g. "2-15 g: sslggkdglqgxpgkx"
            // Password is valid if the specified letter appears exactly once in one of the two 1-based positions given
            bool isValid = false;
            string[] parts = passwordDefn.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
            int index1 = int.Parse(parts[0]) - 1;
            int index2 = int.Parse(parts[1]) - 1;
            char letter = parts[2][0];
            string pw = parts[3];

            for (int i = 0; i < pw.Length; i++)
            {
                if (pw[i] == letter && (i == index1 || i == index2))
                {
                    if (isValid)
                    {
                        isValid = false;
                        break;
                    }
                    isValid = true;
                }
            }
            return isValid;
        }
    }
}
