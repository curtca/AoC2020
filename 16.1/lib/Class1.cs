using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

public class FieldDef
{
    public string name;
    public Range[] ranges;
    public FieldDef(string input)
    {
        string[] parts = input.Split(new string[] { ": ", " or ", "-" }, StringSplitOptions.None);

        this.name = parts[0];
        ranges = new Range[2] { new Range(int.Parse(parts[1]), int.Parse(parts[2])), 
                                new Range(int.Parse(parts[3]), int.Parse(parts[4])) };
    }

    internal bool CouldBeValid(int v)
    {
        bool valid = false;
        foreach (var r in ranges)
        {
            if (v >= r.Start.Value && v <= r.End.Value)
                valid = true;
        }
        return valid;
    }
}


public class Tickets
{
    List<FieldDef> fieldDefs = new List<FieldDef>();
    int[] myTicket;
    int[][] nearbyTickets;
    public Tickets(string input)
    {
        var sections = input.Split("\r\n\r\n");
        foreach (string strFD in sections[0].Split("\r\n"))
            fieldDefs.Add(new FieldDef(strFD));

        myTicket = (from str in sections[1].Split("\r\n")[1].Split(',')
                   select int.Parse(str)).ToArray();

        nearbyTickets = sections[2].Split("\r\n").Skip(1).Select(line =>
            line.Split(',').Select(v => int.Parse(v)).ToArray()
        ).ToArray();
    }

    public long Errors()
    {
        long cerrors = 0;
        foreach (var ticket in nearbyTickets)
        {
            foreach (var field in ticket)
            {
                bool valid = false;
                foreach (var fdef in fieldDefs)
                    valid = valid || fdef.CouldBeValid(field);

                cerrors += valid ? 0 : field;
            }
        }
        return cerrors;
    }
}