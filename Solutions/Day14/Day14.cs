using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day14
    {
        static readonly int Date = 14;
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
            return SpinCycle(input); ;
        }

        private long SolvePartOne(string[] input)
        {
            long resultPartOne = 0;
            for (int index = 0; index < input[0].Length; index++)
            {
                StringBuilder sb = new StringBuilder();
                List<int> cubePositions = new List<int>();
                int counter = input.Length;
                foreach (string line in input)
                {
                    if (line[index] == '#')
                        cubePositions.Add(counter);

                    counter--;
                    if (line[index] != '.')
                        sb.Append(line[index]);
                }
                resultPartOne += AddLoad(sb.ToString(), cubePositions.ToArray(), input.Length);
            }

            return resultPartOne;
        }

        private long AddLoad(string load, int[] cubePositions, int length)
        {
            int totalLoad = 0;
            int cubeIndex = 0;
            foreach (char c in load)
            {
                if (c == 'O')
                {
                    totalLoad += length;
                }
                else if (c == '#' && cubeIndex < cubePositions.Length)
                {
                    length = cubePositions[cubeIndex];
                    cubeIndex++;
                }
                length--;
            }
            return totalLoad;
        }

        private long SpinCycle(string[] input)
        {
            Dictionary<long, List<long>> sums = new();
            long cyclesAtTarget = -1;
            List<string> newPositions = new();
            long cycles = 0;
            while (cyclesAtTarget < 0)
            {
                cycles++;
                for (int spin = 0; spin < 4; spin++)
                {
                    for (int charPos = 0; charPos < input[0].Length; charPos++)
                    {
                        StringBuilder sb = new StringBuilder();
                        List<int> cubePositions = new List<int>();

                        for (int line = 0; line < input.Length; line++)
                        {
                            if (input[line][charPos] == '#')
                                cubePositions.Add(line);


                            if (input[line][charPos] != '.')
                                sb.Append(input[line][charPos]);
                        }
                        if (cubePositions.Count == 0)
                        {
                            cubePositions.Add(input.Length);
                        }
                        string newColumn = NewPositions(sb.ToString(), cubePositions.ToArray(), input.Length);
                        char[] temp = newColumn.ToCharArray();
                        Array.Reverse(temp);
                        newPositions.Add(new string(temp));
                    }

                    input = newPositions.ToArray();
                    newPositions.Clear();

                }
                long key = LoadPartTwo(input);

                if (!sums.ContainsKey(key))
                    sums.Add(key, new List<long> { cycles });
                else
                {
                    sums[key].Add(cycles);
                }

                if (sums[key].Count == 3 && sums[key][2] - sums[key][1] == sums[key][1] - sums[key][0])
                {
                    cyclesAtTarget = ((1000000000 - sums[key][0]) % (sums[key][1] - sums[key][0])) + sums[key][0];

                }

            }
            return sums.First(x => x.Value.Contains(cyclesAtTarget)).Key;
        }

        private long LoadPartTwo(string[] input)
        {
            long result = 0;
            int length = input.Length;
            foreach (var part in input)
            {
                foreach (var c in part)
                {
                    if (c == 'O')
                    {
                        result += length;
                    }
                }
                length--;
            }
            return result;
        }

        private string NewPositions(string load, int[] cubePositions, int length)
        {
            int loadIndex = 0;
            int cubeIndex = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if (loadIndex < load.Length && load[loadIndex] == 'O')
                {
                    sb.Append('O');
                    loadIndex++;
                }
                else if (loadIndex < load.Length && load[loadIndex] == '#' && cubeIndex < cubePositions.Length && i == cubePositions[cubeIndex])
                {
                    sb.Append('#');
                    cubeIndex++;
                    loadIndex++;
                }
                else
                {
                    sb.Append('.');
                }
            }
            return sb.ToString();
        }
    }
}
