using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day12
    {
        static readonly int Date = 12;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = 0;
            long p2 = 0;

            foreach (var line in input)
            {
                var parts = line.Split(' ');
                string pattern = parts[0];
                string unfoldedPattern = string.Join("?", Enumerable.Repeat(pattern, 5));
                int[] groups = parts[1].Split(",").Select(int.Parse).ToArray();
                List<int> unfoldedGroups = new();
                for (int i = 0; i < 5; i++)
                {
                    unfoldedGroups.AddRange(groups);
                }
                p1 += GetArrangementCount(pattern, groups);
                p2 += GetArrangementCount(unfoldedPattern, unfoldedGroups.ToArray());

            }

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private long SolvePartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private long SolvePartOne(string[] input)
        {
            throw new NotImplementedException();
        }

        static long GetArrangementCount(string pattern, int[] groups)
        {
            var arrangements = new long[pattern.Length + 1, groups.Length + 1];
            arrangements[0, 0] = 1;


            for (int patternLength = 1; patternLength <= pattern.Length; patternLength++)
            {
                int patternIndex = patternLength - 1;
                for (int groupCount = 0; groupCount <= groups.Length; groupCount++)
                {
                    var character = pattern[patternIndex];
                    if (character == '.' || character == '?')
                    {
                        arrangements[patternLength, groupCount] += arrangements[patternLength - 1, groupCount];

                    }

                    if (character == '#' || character == '?')
                    {
                        if (groupCount == 0)
                        {

                            continue;
                        }

                        // The last group must end with this character
                        var groupSize = groups[groupCount - 1];
                        if (patternLength < groupSize)
                        {
                            arrangements[patternLength, groupCount] = 0;
                            continue;
                        }

                        bool canPlaceGroup = true;
                        for (int endIndex = patternIndex; endIndex >= patternIndex - groupSize + 1; endIndex--)
                        {
                            if (pattern[endIndex] == '.')
                            {
                                canPlaceGroup = false; break;
                            }
                        }

                        //There must be a non-# before the group
                        if (patternIndex - groupSize >= 0 && pattern[patternIndex - groupSize] == '#')
                        {
                            canPlaceGroup = false;
                        }

                        if (canPlaceGroup)
                        {
                            if (patternLength == groupSize)
                            {
                                if (groupCount == 1)
                                    arrangements[patternLength, groupCount] += 1;
                            }
                            else
                            {
                                arrangements[patternLength, groupCount] +=
                                    arrangements[patternLength - groupSize - 1, groupCount - 1];

                            }
                        }

                    }
                }
            }
            return arrangements[pattern.Length, groups.Length];
        }
    }
}
