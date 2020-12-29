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
            List<string> matched = Match(rules[id], s);
            bool match = matched.Exists(s => s == "");
            Debug.Print("{0} {1}", match ? "✔" : "✖", lines[i]);

            if (match)
                matches++;
        }

        return matches;
    }

    // Returns: List of remainder strings after successful matches (or null if no matches)
    List<string> Match(Rule rule, string text)
    {
        List<string> remainders = new List<string>();

        if (rule.token != '\0')
        {
            if (text.Length > 0 && text[0] == rule.token)
                remainders.Add(text.Substring(1));
        }
        else
        {
            foreach (int[] ruleids in rule.ruleIDs)         // the options for matching this rule
            {
                List<string> ways = new List<string>();
                ways.Add(text);                             // only one way to get to starting rule
                for (int i = 0; i < ruleids.Length; i++)    // each of the rules in this option
                {
                    List<string> waysnext = new List<string>();
                    foreach (string way in ways)            // ways to get to this point, in terms of remaining string
                    {
                        waysnext.AddRange(Match(rules[ruleids[i]], way));
                    }
                    ways = waysnext;
                }
                // we finished all the rules for this option. We got a match
                foreach (string possibility in ways)
                    remainders.Add(possibility);
            }
        }
        return remainders;
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
