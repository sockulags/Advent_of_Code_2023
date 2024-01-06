using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day6
    {
        static readonly int Date = 6;
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
            Regex rx = new Regex(@"\d+");
            long time = SortInputProblem2(rx.Matches(input[0]));
            long distance = SortInputProblem2(rx.Matches(input[1]));
            return PossibleRecords((int)time, (int)distance);
        }

        private long SolvePartOne(string[] input)
        {
            int sum = 1;
            Regex rx = new Regex(@"\d+");
            List<int> times = SortInputProblem1(rx.Matches(input[0]));
            List<int> distance = SortInputProblem1(rx.Matches(input[1]));

            for (int i = 0; i < times.Count; i++)
            {
                sum *= PossibleRecords(times[i], distance[i]);
            }
            return sum;
        }

        public int PossibleRecords(int time, int distance)
        {
            int recordsBroken = 0;
            for (int i = 0; i < time; i++)
            {
                int traveledDistance = i * (time - i);
                if (traveledDistance > distance)
                {
                    recordsBroken++;
                }
            }
            return recordsBroken;
        }

        public List<int> SortInputProblem1(MatchCollection matchCollection)
        {
            List<int> data = new List<int>();
            foreach (Match match in matchCollection)
            {
                if (match.Length > 0)
                    data.Add(int.Parse(match.Value));
            }

            return data;
        }
        public long SortInputProblem2(MatchCollection matchCollection)
        {
            string data = "";
            foreach (Match match in matchCollection)
            {
                if (match.Length > 0)
                    data += match.Value;
            }

            return long.Parse(data);
        }
    }
}
