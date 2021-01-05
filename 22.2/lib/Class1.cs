using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.IO;

public class War
{
    Game g = null;
    public War(string input)
    {
        g = new Game(
            input.Split(new string[] { "Player 1:\r\n", "\r\n\r\nPlayer 2:\r\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new Queue<int>(p.Split("\r\n").Select(int.Parse).ToList())).ToArray());
    }

    public long WinningScore()
    {
        long score = 0;
        int winner = g.Play();
        score = g.Score(winner);
        return score;
    }
}

public class Game
{
    static int game = 0;
    Queue<int>[] players;
    List<Queue<int>[]> history = new List<Queue<int>[]>();

    public Game(Queue<int>[] players)
    {
        this.players = players;
    }

    public int Play() // returns winning player (0/1)
    {
        int winner = 0;
        while (players[0].Count > 0 && players[1].Count > 0)
        {
            if (BeenHere())
            {
                winner = 0;
                break;
            }

            history.Add(new Queue<int>[] { new Queue<int>(players[0]), new Queue<int>(players[1]) });
            var played = players.Select(p => p.Dequeue()).ToArray();
            if (players[0].Count >= played[0] && players[1].Count >= played[1])
                winner = (new Game(new Queue<int>[] { 
                    new Queue<int>(players[0].Take(played[0])), 
                    new Queue<int>(players[1].Take(played[1]))
                })).Play();
            else
                winner = played[0] > played[1] ? 0 : 1;
            players[winner].Enqueue(played[winner]);
            players[winner].Enqueue(played[1 - winner]);
        }
        return winner;
    }

    private bool BeenHere()
    {
        bool found = history.Any(prev => 
            prev[0].SequenceEqual(players[0]) && 
            prev[1].SequenceEqual(players[1])
        );
        return found;
    }

    public long Score(int player)
    {
        int numcards = players[player].Count;
        return Enumerable.Range(0, numcards).Aggregate(0, (a, n) =>
            a += (numcards - n) * players[player].ElementAt(n));
    }
}