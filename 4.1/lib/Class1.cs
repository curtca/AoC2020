using System;

namespace lib
{
    public class Passports
    {
        static string[] codes = new string[] {
            "byr",
            "iyr", 
            "eyr", 
            "hgt", 
            "hcl", 
            "ecl", 
            "pid", 
            "cid", };

        public int CountValid(string input)
        {
            string[] passports = input.Split("\r\n\r\n");
            int countValid = 0;

            for (int i = 0; i < passports.Length; i++)
            {
                if (IsValid(passports[i]))
                    countValid++;
            }


            return countValid;
        }

        private bool IsValid(string passport)
        {
            string[] fields = passport.Split(new string[] { "\r\n", " " }, StringSplitOptions.None);
            bool[] present = new bool[codes.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                string[] field = fields[i].Split(':');
                present[Array.IndexOf(codes, field[0])] = true;
            }
            for (int i = 0; i < present.Length; i++)
            {
                if (codes[i] != "cid" && !present[i])
                    return false;
            }
            return true;
        }
    }
}
