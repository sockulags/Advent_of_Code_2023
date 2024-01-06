
using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day7
    {
        static readonly int Date = 7;
        string InputSource = $"Day{Date}";

        public bool isPartOne = true ;

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = SolvePartOne(input);
            long p2 = SolvePartTwo(input);

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private long SolvePartTwo(string[] input)
        {
            isPartOne = false;
            List<PokerHand> Hands = new List<PokerHand>();
            foreach (var item in input)
            {
                int handValue = 0;
                int cardValue = 0;

                string[] data = item.Split(' ');
                List<int> hand = new List<int>();
                List<int> sortedHand = new List<int>();
                foreach (char c in data[0])
                {
                    hand.Add(SortHand(c));
                }
                for (int i = 2; i <= 14; i++)
                {
                    List<int> jokerHands = new List<int>();
                    foreach (var nr in hand)
                    {
                        if (nr == 1)
                            jokerHands.Add(i);
                        else
                            jokerHands.Add(nr);
                    }
                    int jokerHand = GetHandValue(jokerHands);
                    if (jokerHand > handValue)
                        handValue = jokerHand;
                }

                Hands.Add(new PokerHand
                {
                    HandValue = handValue,
                    BetSize = int.Parse(data[1]),
                    CardValues = hand
                });

            }
            int sum = 0;
            int place = 1;

            var sortByCardVal = Hands.
                OrderBy(x => x.HandValue).
                ThenBy(x => x.CardValues[0]).
                ThenBy(x => x.CardValues[1]).
                ThenBy(x => x.CardValues[2]).
                ThenBy(x => x.CardValues[3]).
                ThenBy(x => x.CardValues[4]).
                ToList();
            foreach (var item in sortByCardVal)
            {
                sum += item.BetSize * place;
                place++;
            }
            return sum;

        }

        private long SolvePartOne(string[] input)
        {
            List<PokerHand> Hands = new List<PokerHand>();
            foreach (var item in input)
            {
                string[] data = item.Split(' ');
                List<int> hand = new List<int>();
                foreach (char c in data[0])
                {
                    hand.Add(SortHand(c));
                }
                Hands.Add(new PokerHand
                {
                    HandValue = GetHandValue(hand),
                    BetSize = int.Parse(data[1]),
                    CardValues = hand
                });

            }
            int sum = 0;
            int place = 1;

            var sortByCardVal = Hands.
                OrderBy(x => x.HandValue).
                ThenBy(x => x.CardValues[0]).
                ThenBy(x => x.CardValues[1]).
                ThenBy(x => x.CardValues[2]).
                ThenBy(x => x.CardValues[3]).
                ThenBy(x => x.CardValues[4]).
                ToList();


            foreach (var item in sortByCardVal)
            {
                sum += item.BetSize * place;
                place++;
            }
            return sum;
        }

        private int GetHandValue(List<int> hand)
        {
            var groupedChunks = hand.OrderByDescending(x => x).GroupBy(x => x).ToList();
            int handvalue = 0;
            foreach (var groupedChunk in groupedChunks)
            {
                switch (groupedChunk.Count())
                {
                    case 1: handvalue += 0; break;
                    case 2: handvalue += 2; break;
                    case 3: handvalue += 5; break;
                    case 4: handvalue += 8; break;
                    case 5: handvalue += 13; break;
                    default: handvalue += 0; break;
                }

            }
            if (groupedChunks.Count() == 1)
                handvalue *= 3;
            if (groupedChunks.Count() == 2)
                handvalue *= 2;
            return handvalue;
        }

        public int SortHand(char c)
        {
            switch (c)
            {
                case 'A': return 14;
                case 'K': return 13;
                case 'Q': return 12;
                case 'J': return isPartOne ? 11 : 1;
                case 'T': return 10;
                case '9': return 9;
                case '8': return 8;
                case '7': return 7;
                case '6': return 6;
                case '5': return 5;
                case '4': return 4;
                case '3': return 3;
                case '2': return 2;

                default: return 0;
            }
        }
    }
}
