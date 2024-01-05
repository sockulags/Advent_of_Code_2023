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
        public int extraLoops = 0;
        public int score = 0;
        private long SolvePartTwo(string[] input)
        {
            List<string> inputloop = [.. input];
            ScoreCounter(inputloop, inputloop.Count(), 0);
            return score;
        }
        public void ScoreCounter(List<string> inputloop, int loops, int startValue)
        {
            for (int i = startValue; i < loops + startValue; i++)
            {
                extraLoops = 0;
                string[] data = inputloop[i].Replace(" ", ".").Split(':', '|');
                string[] winningNrs = data[1].Split('.');
                string[] ticketNrs = data[2].Split('.');

                for (int j = 0; j < winningNrs.Length; j++)
                {
                    if (winningNrs[j].Length > 0)
                    {
                        for (int k = 0; k < ticketNrs.Length; k++)
                        {
                            if (winningNrs[j] == ticketNrs[k])
                            {
                                ticketNrs[k] = "";
                                extraLoops++;
                            }
                        }
                    }
                }
                score++;
                if (extraLoops > 0)
                    ScoreCounter(inputloop, extraLoops, i + 1);
            }
        }

        private long SolvePartOne(string[] input)
        {
            int currentScore = 0;
            int score = 0;
            foreach (var line in input)
            {
                string[] data = line.Replace(" ", ".").Split(':', '|');
                string[] winningNrs = data[1].Split('.');
                string[] nrs = data[2].Split('.');

                foreach (var win in winningNrs)
                {
                    if (win.Length > 0)
                        foreach (var nr in nrs)
                        {
                            if (nr == win)
                            {
                                nrs.Where(x => x == nr).First().Replace(nr, "00");
                                currentScore += currentScore == 0 ? 1 : currentScore;
                            }
                        }
                }
                score += currentScore;
                currentScore = 0;
            }
            return score;
        }
    }
}
