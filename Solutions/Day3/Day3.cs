using SockulagsClassLibrary;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Advent_of_Code_2023.Solutions
{
    public class Day3
    {
        static readonly int Date = 3;
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
            int sum = 0;
            for (int row = 0; row < input.Length; row++)
            {
                Regex regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(input[row]))
                {
                    if (CheckValues(match, row, input, input.Length))
                        sum += int.Parse(match.Value.ToString());
                }
            }
            return sum;
        }

        public int gearKey = 1;
        private long SolvePartTwo(string[] input)
        {
            int sum = 0;
            Dictionary<string, int> gears = new Dictionary<string, int>();
           
            for (int row = 0; row < input.Length; row++)
            {
                Regex regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(input[row]))
                {
                    string key = CheckGears(match, row, input, input.Length);
                    if (key.Length > 0)
                        gears.Add(key, int.Parse(match.Value));
                }
            }
            foreach (var gear in gears.GroupBy(x => x.Key.Substring(4)).ToList())
            {
                if (gear.Count() > 1)
                {
                    var value = gear.First().Value * gear.Last().Value;
                    sum += value;
                }
            }
            return sum;
        }

        private string CheckGears(Match match, int row, string[] input, int length)
        {
            for (int j = row - 1; j < row + 2; j++)
            {
                for (int i = match.Index - 1; i < match.Length + match.Index + 1; i++)
                {
                    if (i < length && j < length && i >= 0 && j >= 0 && !char.IsDigit(input[j][i]) && !char.Equals(input[j][i], '.'))
                    {
                        string key = gearKey < 10 ? $"000{gearKey}" : gearKey < 100 ? $"00{gearKey}" : gearKey < 1000 ? $"0{gearKey}" : gearKey.ToString();
                        gearKey++;
                        return $"{key}ss{j}ss{i}";
                    }
                }
            }          
            return "";
        }

        private bool CheckValues(Match match, int row, string[] input, int length)
        {
            for (int j = row - 1; j < row + 2; j++)
            {
                for (int i = match.Index - 1; i < match.Length + match.Index + 1; i++)
                {
                    if (i < length && j < length && i >= 0 && j >= 0 && !char.IsDigit(input[j][i]) && !char.Equals(input[j][i], '.'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
