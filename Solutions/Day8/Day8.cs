using SockulagsClassLibrary;
using System.Diagnostics;
using Advent_of_Code_2023.HelperClass;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2023.Solutions
{
    public class Day8
    {
        static readonly int Date = 8;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = SolvePartOne(input);
            long p2 = SolvePartTwo(input);

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }
        private long SolvePartOne(string[] input)
        {
            Dictionary<string, (string left, string right)> Nodes = new();
            Regex rx = new Regex(@"(?i)[a-z]+");

            for (int i = 2; i < input.Length; i++)
            {
                Nodes.Add(rx.Matches(input[i])[0].Value, (rx.Matches(input[i])[1].Value, rx.Matches(input[i])[2].Value));
            }
            return long.Parse(TotalSteps(Nodes, input[0]));
        }
        public static string TotalSteps(Dictionary<string, (string left, string right)> nodes, string path)
        {
            string current = "AAA";
            int steps = 0;
            int i = 0;
            while (current != "ZZZ")
            {
                current = path[i] == 'L' ? nodes[current].left : nodes[current].right;
                i = (i + 1) % path.Length;
                steps++;
            }
            return steps.ToString();
        }

        private long SolvePartTwo(string[] input)
        {
            Dictionary<string, (string left, string right)> Nodes = new();
            Regex rx = new Regex(@"(?i)[a-z]+");

            for (int i = 2; i < input.Length; i++)
            {
                Nodes.Add(rx.Matches(input[i])[0].Value, (rx.Matches(input[i])[1].Value, rx.Matches(input[i])[2].Value));
            }
            var starts = Nodes.Keys.Where(x => x.EndsWith('A')).ToList();

            List<long> totalSteps = new List<long>();
            foreach (var start in starts)
            {
                totalSteps.Add(TotalStepsPartTwo(Nodes, input[0], start));
            }

            return totalSteps.LeastCommonMultiple();
        }

        public static long TotalStepsPartTwo(Dictionary<string, (string left, string right)> nodes, string path, string current)
        {
            long rotations = 0;
            int i = 0;

            while (!current.EndsWith('Z'))
            {
                current = path[i] == 'L' ? nodes[current].left : nodes[current].right;
                i = (i + 1) % path.Length;
                rotations++;
            }
            return rotations;

        }


    }
}
