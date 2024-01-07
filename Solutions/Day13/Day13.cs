using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day13
    {
        // Store row/columns to skip on part 2
        static long partOneHorizontal = 0;
        static long partOneVertical = 0;

        static readonly int Date = 13;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);

            long partOneResult = 0;
            long partTwoResult = 0;

            List<string> currentMap = new List<string>();
            foreach (var line in input)
            {
                if (line == string.Empty)
                {
                    partOneResult += SolvePartOne(currentMap.ToArray());
                    partTwoResult += SolvePartTwo(currentMap.ToArray());
                    currentMap.Clear();
                }
                else
                {
                    currentMap.Add(line);
                }
            }
            partOneResult += SolvePartOne(currentMap.ToArray());
            partTwoResult += SolvePartTwo(currentMap.ToArray());

            return Day.Answer(Date, partOneResult, partTwoResult, sw.ElapsedMilliseconds);
        }

        private long SolvePartTwo(string[] map)
        {
            long partTwoHorizontal = FindReflectionPartTwo(map, partOneHorizontal);
            long partTwoVertical = FindReflectionPartTwo(VerticalFlip(map), partOneVertical);
            return partTwoHorizontal > 0 ? partTwoHorizontal * 100 : partTwoVertical;
        }

        private long SolvePartOne(string[] map)
        {
            partOneVertical = FindReflectionPartOne(VerticalFlip(map));
            partOneHorizontal = FindReflectionPartOne(map);
            return partOneHorizontal > 0 ? partOneHorizontal * 100 : partOneVertical;
        }

        private static long FindReflectionPartOne(string[] map)
        {
            for (int row = 1; row < map.Length; row++)
            {
                if (map[row - 1] == map[row])
                {
                    bool isMirror = true;
                    for (int startIndex = 0; startIndex < map.Length - row; startIndex++)
                    {
                        if (row - startIndex - 1 >= 0 && map[row - startIndex - 1] != map[row + startIndex])
                        {
                            isMirror = false;
                        }
                    }
                    if (isMirror)
                        return row;
                }
            }
            return 0;
        }

        private static long FindReflectionPartTwo(string[] map, long partOne)
        {
            for (int row = 1; row < map.Length; row++)
            {
                if (row == (int)partOne) //Skip part one reflection
                    row++;

                if (row == map.Length)
                    break;

                bool isMirror = false;
                if (map[row - 1] == map[row] || CompareStrings(map[row - 1], map[row]))
                {
                    isMirror = true;

                    //If this doesn't reset isMirror to false, then row = the reflection distnace left/up
                    for (int startIndex = 1; startIndex < map.Length - row + 1; startIndex++)
                    {
                        if (row - startIndex - 1 < 0 || startIndex >= map.Length - row)
                            break;

                        // If any reflections isn't a match, check if the there is smudge on the mirror.
                        if (map[row - startIndex - 1] != map[row + startIndex])
                            isMirror = CompareStrings(map[row - startIndex - 1], map[row + startIndex]);
                    }
                }
                if (isMirror)
                    return row;
            }
            return 0;
        }

        private static string[] VerticalFlip(string[] map)
        {
            List<string> verticalMap = new List<string>();
            for (int col = 0; col < map[0].Length; col++)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var line in map)
                {
                    sb.Append(line[col]);
                }
                verticalMap.Add(sb.ToString());
            }
            return verticalMap.ToArray();
        }

        private static bool CompareStrings(string curr, string next)
        {
            int counter = 0; ;
            for (int i = 0; i < curr.Length; i++)
            {
                if (curr[i] != next[i])
                    counter++;

                if (counter > 1)
                    return false;
            }
            return counter == 1;
        }
    }
}
