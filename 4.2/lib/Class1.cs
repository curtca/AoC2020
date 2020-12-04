using System;
using System.Collections.Generic;

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
            var fieldsMap = new Dictionary<string, string>(); // code -> val
            string[] fields = passport.Split(new string[] { "\r\n", " " }, StringSplitOptions.None);
            for (int i = 0; i < fields.Length; i++)
            {
                string[] field = fields[i].Split(':');
                fieldsMap.Add(field[0], field[1]);
            }
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i] == "cid")
                    continue;

                if (!fieldsMap.ContainsKey(codes[i]))
                    return false;

                var val = fieldsMap[codes[i]];
                switch (codes[i])
                {
                    case "byr":
                        int byr = int.Parse(val);
                        if (byr < 1920 || byr > 2002)
                            return false;
                        break;
                    case "iyr":
                        int iyr = int.Parse(val);
                        if (iyr < 2010 || iyr > 2020)
                            return false;
                        break;
                    case "eyr":
                        int eyr = int.Parse(val);
                        if (eyr < 2020 || eyr > 2030)
                            return false;
                        break;
                    case "hgt":
                        if (val.EndsWith("cm"))
                        {
                            int cm = int.Parse(val.Substring(0, val.Length - 2));
                            if (cm < 150 || cm > 193)
                                return false;
                        }
                        else if (val.EndsWith("in"))
                        {
                            int inch = int.Parse(val.Substring(0, val.Length - 2));
                            if (inch < 59 || inch > 76)
                                return false;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "hcl":
                        if (val[0] != '#' || val.Length != 7)
                            return false;
                        int hcl = 0;
                        if (!int.TryParse(val.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out hcl))
                            return false;
                        break;
                    case "ecl":
                        string[] colors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                        if (!Array.Exists(colors, e => e == val))
                            return false;
                        break;
                    case "pid":
                        int pid = 0;
                        if (val.Length != 9 || !int.TryParse(val, out pid))
                            return false;
                        break;
                    case "cid":
                        break;
                }
            }
            return true;
        }
    }
}
