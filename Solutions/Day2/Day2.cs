using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions.Day2
{
    public class Day2
    {
        static readonly int Date = 2;
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
            int counter = 0;
            int sum = 0;
            bool check;
            foreach (string line in input)
            {
                check = true;
                counter++;
                string[] sepLines = line.Split(':', ';');
                sepLines[0] = counter.ToString();

                for (int i = 1; i < sepLines.Length; i++)
                {
                    string[] uniqueInput = sepLines[i].Split(' ');
                    if (!Check(uniqueInput))
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                    sum += counter;

            }
            return sum;
        }

        static long SolvePartTwo(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                int red = 0, blue = 0, green = 0;
                string[] sepLines = line.Split(':', ';');

                for (int i = 1; i < sepLines.Length; i++)
                {
                    string[] uniqueInput = sepLines[i].Split(' ');
                    for (int j = 1; j < uniqueInput.Length; j++)
                    {
                        if (uniqueInput[j].StartsWith("red") && int.Parse(uniqueInput[j - 1]) > red)
                            red =  int.Parse(uniqueInput[j - 1]);

                        if (uniqueInput[j].StartsWith("blue") && int.Parse(uniqueInput[j - 1]) > blue)
                            blue = int.Parse(uniqueInput[j - 1]);

                        if (uniqueInput[j].StartsWith("green") && int.Parse(uniqueInput[j - 1]) > green)
                            green = int.Parse(uniqueInput[j - 1]);
                    }
                }
                sum += red * green * blue;
            }
            return sum;
        }
    

        static bool Check(string[] input)
        {
            int red = 12;
            int green = 13;
            int blue = 14;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i].StartsWith("red"))
                    red -= int.Parse(input[i - 1]);

                if (input[i].StartsWith("blue"))
                    blue -= int.Parse(input[i - 1]);

                if (input[i].StartsWith("green"))
                    green -= int.Parse(input[i - 1]);
            }
            if (red >= 0 && green >= 0 && blue >= 0)
                return true;

            return false;
        }
    }
}
