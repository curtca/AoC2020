using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Monster
{
    Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
    string[] lines;
    public Monster(string input)
    {
        string[] parts = input.Split("\r\n\r\n");
        foreach (string rule in parts[0].Split("\r\n"))
        {
            string[] strrules = rule.Split(": ");
            rules[int.Parse(strrules[0])] = new Rule(strrules[1]);
        }

        lines = parts[1].Split("\r\n");
    }

    public int CountMatches(int id)
    {
        int matches = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i];
            if (Match(rules[id], ref s) && String.IsNullOrEmpty(s))
                matches++;
        }

        return matches;
    }

    bool Match(Rule rule, ref string text)
    {
        if (rule.token != '\0')
        {
            if (text.Length > 0 && text[0] == rule.token)
            {
                text = text.Substring(1);
                return true;
            }
        }
        else
        {
            foreach (int[] ruleids in rule.ruleIDs)
            {
                string s = text;
                bool match = true;
                for (int i = 0; i < ruleids.Length; i++)
                {
                    if (!Match(rules[ruleids[i]], ref s))
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    text = s;
                    return true;
                }
            }
        }
        return false;
    }
}

public class Rule
{
    public List<int[]> ruleIDs { get; } = null;
    public char token = '\0';
    public Rule(string def)
    {
        if (!char.TryParse(def, out token))
        {
            string[] strRule = def.Split(" | ");
            ruleIDs = new List<int[]>();
            foreach (string ids in strRule)
                ruleIDs.Add(ids.Split(" ").Select(id => int.Parse(id)).ToArray());
        }
    }
}
