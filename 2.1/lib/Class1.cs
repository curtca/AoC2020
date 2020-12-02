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
            bool isValid = false;
            string[] parts = passwordDefn.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
            int minCount = int.Parse(parts[0]);
            int maxCount = int.Parse(parts[1]);
            char letter = parts[2][0];
            string pw = parts[3];
            int count = 0;
            for (int i = 0; i < pw.Length; i++)
            {
                if (pw[i] == letter)
                    count++;
            }
            isValid = count >= minCount && count <= maxCount;
            return isValid;
        }
    }
}
