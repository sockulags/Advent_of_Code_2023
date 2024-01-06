using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day11
    {
        static readonly int Date = 11;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            List<string> mappedUniverse = new List<string>(MapOutEmptyParts(input));

            long part1 = 2;
            long part2 = 1000000;

            //Find inital value
            long notCountingEmptySpace = CalculateTotalDistance(GalaxiesLocations(mappedUniverse, 1));

            //Find linear difference            
            long diffPerExpansion = notCountingEmptySpace - CalculateTotalDistance(GalaxiesLocations(mappedUniverse, 0));

            //Negative one since the linear difference is one iteration
            long resultPartOne = notCountingEmptySpace + diffPerExpansion * (part1 - 1);
            long resultPartTwo = notCountingEmptySpace + diffPerExpansion * (part2 - 1);

            return Day.Answer(Date, resultPartOne, resultPartTwo, sw.ElapsedMilliseconds);
        }

        static int rowLength = 0;
        static char emptyX = '-';
        static char emptyY = '|';

      
        static long CalculateTotalDistance(long[] galaxyLocations)
        {
            long sum = 0;
            for (int i = 0; i < galaxyLocations.Length; i++)
            {
                sum += SumOfAllPairs(galaxyLocations[i..], galaxyLocations[i]);
            }
            return sum;
        }
        static long SumOfAllPairs(long[] galaxiesLeft, long galaxy)
        {
            if (1 >= galaxiesLeft.Length)
                return 0;

            // Calculate the horizontal and vertical distances (NextGalaxyPos - ThisGalaxyPosition)
            long distX = Math.Abs((galaxiesLeft[1] % rowLength) - (galaxy % rowLength));
            long distY = Math.Abs((galaxiesLeft[1] / rowLength) - (galaxy / rowLength));
            // Calculate the total distance (Manhattan distance)
            long sum = distX + distY;
            return sum + SumOfAllPairs(galaxiesLeft[1..], galaxy);
        }
        static long[] GalaxiesLocations(List<string> mappedUniverse, int n)
        {
            List<long> result = new List<long>();
            long location = -1;

            if (mappedUniverse[0].Contains(emptyY))
            {
                rowLength = (n - 1) * mappedUniverse[0].Count(x => x == emptyY);
            }
            rowLength += mappedUniverse[0].Length;
            foreach (string s in mappedUniverse)
            {
                if (s.Contains(emptyX))
                    location += n * rowLength;
                else
                    foreach (char c in s)
                    {
                        if (c == emptyY)
                            location += n;
                        else
                            location++;

                        if (c == '#')
                            result.Add(location);
                    }

            }
            return result.ToArray();
        }
        static List<string> MapOutEmptyParts(string[] universe)
        {
            List<string> mappedUniverse = new List<string>(ReplaceEmptyHorizontalLines(universe));

            return ReplaceEmptyVerticalLines(mappedUniverse);
        }
        static List<string> ReplaceEmptyVerticalLines(List<string> mappedUniverse)
        {
            for (int j = 0; j < mappedUniverse[0].Length; j++)
            {
                int empty = 0;
                for (int i = 0; i < mappedUniverse.Count; i++)
                {
                    if (mappedUniverse[i][j] == '#')
                    {
                        empty++;
                    }
                }
                if (empty == 0)
                {
                    for (int i = 0; i < mappedUniverse.Count; i++)
                    {
                        char[] a = mappedUniverse[i].ToCharArray();
                        a[j] = emptyY;
                        mappedUniverse[i] = new string(a);
                    }
                }
            }
            return mappedUniverse;
        }
        static List<string> ReplaceEmptyHorizontalLines(string[] universe)
        {
            List<string> mappedHorizontally = new List<string>();
            foreach (string s in universe)
            {
                if (!s.Contains('#'))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < s.Length; i++)
                    {
                        sb.Append(emptyX);
                    }

                    mappedHorizontally.Add(sb.ToString());
                }
                else
                {
                    mappedHorizontally.Add(s);
                }
            }
            return mappedHorizontally;
        }
    }
}
