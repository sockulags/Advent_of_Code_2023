using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Advent_of_Code_2023.Solutions
{
    public class Day4
    {
        static readonly int Date = 4;
        string InputSource = $"Day{Date}";

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
            int[] scratchCards = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                scratchCards[i] = scratchCards[i] + 1;
                int extraCards = 0;
                string[] data = input[i].Replace(" ", ".").Split(':', '|');
                string[] winningNrs = data[1].Split('.').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                string[] ticketNrs = data[2].Split('.').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                extraCards = winningNrs.Count(number => ticketNrs.Contains(number));

                for (int x = i + 1; x <= i + extraCards; x++)
                {
                    scratchCards[x] += scratchCards[i];
                }
            }
            return scratchCards.Sum();
        }
        private long SolvePartOne(string[] input)
        {
            int currentScore = 0;
            int score = 0;
            foreach (var line in input)
            {
                string[] data = line.Replace(" ", ".").Split(':', '|');
                string[] winningNrs = data[1].Split('.').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                string[] ticketNrs = data[2].Split('.').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                score += (int)Math.Pow(2, winningNrs.Count(number => ticketNrs.Contains(number)) - 1);
            }
            return score;
        }
    }
}
