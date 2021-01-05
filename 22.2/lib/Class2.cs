using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day22
{
    public static class Day22
    {
        private const int Day = 22;
        static StreamWriter sw = null;
        static long line = 0;

        /*
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var decks = lines.Split("");
            var player1 = decks.First().Skip(1).Select(int.Parse).ToList();
            var player2 = decks.Last().Skip(1).Select(int.Parse).ToList();

            while (player1.Any() && player2.Any())
            {
                var card1 = player1.First();
                var card2 = player2.First();

                player1 = player1.Skip(1).ToList();
                player2 = player2.Skip(1).ToList();

                var winner = Math.Max(card1, card2);
                var loser = Math.Min(card1, card2);
                var p1Winner = card1 > card2;

                if (p1Winner)
                {
                    player1.Add(winner);
                    player1.Add(loser);
                }
                else
                {
                    player2.Add(winner);
                    player2.Add(loser);
                }
            }

            var winnerDeck = new[] { player1, player2 }.Single(x => x.Count != 0);

            var maxScore = winnerDeck.Count;

            return Enumerable.Range(0, maxScore).Aggregate(0, (acc, x) => acc + (maxScore - x) * winnerDeck[x]);
            return -1;
        }
        */

        public static long Score(List<int> deck)
        {
            var maxScore = deck.Count;
            return Enumerable.Range(0, maxScore).Aggregate(0, (acc, x) => acc + (maxScore - x) * deck[x]);
        }

        public static string DeckAsString(this IList<int> deck)
        {
            return deck.Aggregate("", (a, x) => a + x);
        }

        public static long Part2(string input)
        {
            var decks = input.Split("\r\n\r\n")
                .Select(d => d.Split("\r\n"));

            var p1Deck = decks.First().Skip(1).Select(int.Parse).ToList();
            var p2Deck = decks.Last().Skip(1).Select(int.Parse).ToList();

            (bool, List<int>) PlayGame(List<int> p1InputDeck, List<int> p2InputDeck, int game)
            {
                var seen = new HashSet<(string, string)>();

                var p1GameDeck = p1InputDeck.ToList();
                var p2GameDeck = p2InputDeck.ToList();

                (string, string) DecksAsTuple() => (p1GameDeck.DeckAsString(), p2GameDeck.DeckAsString());
                int round = 0;
                while (true)
                {
                    round++;
                    var deckTuple = DecksAsTuple();

                    if (line < 1000)
                        sw.Write(p1GameDeck.Aggregate("  $", (a, c) => a += c.ToString() + ",") + Environment.NewLine +
                                 p2GameDeck.Aggregate("  $", (a, c) => a += c.ToString() + ",") + Environment.NewLine);

                    if (seen.Contains(deckTuple))
                    {
                        if (line < 1000)
                            sw.WriteLine($"{line}: Game {game}, round {round}: Been here. P1 wins.");
                        line++;
                        return (true, p1GameDeck);
                    }

                    seen.Add(deckTuple);

                    var card1 = p1GameDeck.First();
                    var card2 = p2GameDeck.First();

                    p1GameDeck = p1GameDeck.Skip(1).ToList();
                    p2GameDeck = p2GameDeck.Skip(1).ToList();

                    bool CalculateRoundWinner()
                    {
                        if (p1GameDeck.Count >= card1 && p2GameDeck.Count >= card2)
                        {
                            return PlayGame(
                                    p1GameDeck.Take(card1).ToList(),
                                    p2GameDeck.Take(card2).ToList(), game + 1)
                                .Item1;
                        }
                        else
                        {
                            return card1 > card2;
                        }
                    }

                    string msg = null;
                    if (CalculateRoundWinner())
                    {
                        p1GameDeck.Add(card1);
                        p1GameDeck.Add(card2);
                        msg = $"{line++}: Game {game}, round {round}: {card1}W {card2}";
                    }
                    else
                    {
                        p2GameDeck.Add(card2);
                        p2GameDeck.Add(card1);
                        msg = $"{line++}: Game {game}, round {round}: {card1} {card2}W";
                    }

                    if (line < 1000)
                        sw.WriteLine(msg + Environment.NewLine);

                    if (!p1GameDeck.Any())
                    {
                        return (false, p2GameDeck);
                    }

                    if (!p2GameDeck.Any())
                    {
                        return (true, p1GameDeck);
                    }
                }
            }

            long score = 0;
            using (sw = new StreamWriter("2.dat"))
            {
                (var winner, var winnerDeck) = PlayGame(p1Deck, p2Deck, 1);
                score = Score(winnerDeck);
                sw.WriteLine($"{line++}: Winning score (player {winner}): {score}");
            }

            return score;
        }
    }
}