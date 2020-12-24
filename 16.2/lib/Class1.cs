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
    FieldDef[] fieldDefs;
    int[] myTicket;
    int[][] nearbyTickets; // [ticket#][position] = field value
    bool[,] possibleFields; // [position#, field#] = possible? (across all tickets)
    public Tickets(string input)
    {
        var sections = input.Split("\r\n\r\n");
        fieldDefs = sections[0].Split("\r\n").Select(line => new FieldDef(line)).ToArray();
        myTicket = sections[1].Split("\r\n")[1].Split(',').Select(n => int.Parse(n)).ToArray();

        var nearbyunparsed = sections[2].Split("\r\n").Skip(1);
        var nearbyasarrays = nearbyunparsed.Select(line =>
            line.Split(',').Select(v => int.Parse(v)).ToArray());
        nearbyTickets = nearbyasarrays.Where(t => CouldBeValid(t)).ToArray();

        // For every position in every ticket, which field(s) could it be?
        possibleFields = new bool[fieldDefs.Length, fieldDefs.Length];
        for (int position = 0; position < nearbyTickets[0].Length; position++)
        {
            for (int ifield = 0; ifield < fieldDefs.Length; ifield++)
            {
                possibleFields[position, ifield] = true;
                for (int inearby = 0; inearby < nearbyTickets.Length; inearby++)
                    possibleFields[position, ifield] = possibleFields[position, ifield] 
                        && fieldDefs[ifield].CouldBeValid(nearbyTickets[inearby][position]);
            }
        }
    }

    public bool CouldBeValid(int[] ticket)
    {
        foreach (var field in ticket)
        {
            bool valid = false;
            foreach (var fdef in fieldDefs)
                valid = valid || fdef.CouldBeValid(field);
            if (!valid)
                return false;
        }
        return true;
    }

    public long DepartureProduct()
    {
        // DOesn't actually work. Just looked at debug output and solved manually.
        long p = 0;

        Debug.Print("Rows = Positions, Columns = Field indices");
        
        // header
        Debug.Write("  ");
        for (int ifield = 0; ifield < fieldDefs.Length; ifield++)
        {
            Debug.Write($"{ifield,3}");
        }
        Debug.Print("");

        for (int ipos = 0; ipos < fieldDefs.Length; ipos++)
        {
            Debug.Write($"{ipos,2}:");
            for (int ifield = 0; ifield < fieldDefs.Length; ifield++)
            {
                Debug.Write(possibleFields[ipos, ifield] ? " * " : "   ");
            }
            Debug.Print("");
        }

        return p;
    }
}