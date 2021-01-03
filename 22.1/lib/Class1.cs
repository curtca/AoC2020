using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class War
{
    Queue<int>[] players;

    public War(string input)
    {
        players = input.Split(new string[] { "Player 1:\r\n", "\r\n\r\nPlayer 2:\r\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new Queue<int>(p.Split("\r\n").Select(c => int.Parse(c)).ToList())).ToArray();
    }

    public long WinningScore()
    {
        int winner = 0;
        while (players[0].Count > 0 && players[1].Count > 0)
        {
            var played = players.Select(p => p.Dequeue()).ToArray();
            winner = played[0] > played[1] ? 0 : 1;
            players[winner].Enqueue(played[winner]);
            players[winner].Enqueue(played[1 - winner]);
        }

        long score = 0;
        int numcards = players[winner].Count;
        for (int i = 1; i <= numcards; i++)
        {
            score += players[winner].ElementAt(numcards - i) * i;
        }
        return score;
    }
}

