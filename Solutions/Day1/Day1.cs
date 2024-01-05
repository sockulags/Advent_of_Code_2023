using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions.Day1
{
    public class Day1
    {
        static readonly int Date = 1;
        string InputSource = $"Day{Date}";
        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = SolvePartOne(input);
            long p2 = SolvePartTwo(input);

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        static long SolvePartOne(string[] input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                var numbers = line.Where(char.IsDigit).ToArray();
                sum += long.Parse($"{numbers[0]}{numbers[^1]}");
            }
            return sum;
        }

        static long SolvePartTwo(string[] input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                StringBuilder sb = new(line);
                sb.Replace("one", "o1e");
                sb.Replace("two", "t2o");
                sb.Replace("three", "t3e");
                sb.Replace("four", "f4r");
                sb.Replace("five", "f5e");
                sb.Replace("six", "s6x");
                sb.Replace("seven", "s7n");
                sb.Replace("eight", "e8t");
                sb.Replace("nine", "n9e");

                var numbers = sb.ToString().Where(char.IsDigit).ToArray();
                sum += long.Parse($"{numbers[0]}{numbers[^1]}");
            }
            return sum;
        }
    }
}
