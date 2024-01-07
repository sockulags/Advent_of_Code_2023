using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public record Point(long x, long y);
    public class Day18
    {
        static readonly int Date = 18;
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
            var instructionPartTwo = input.Select(ParseLine).ToList();
            return CalculateArea(instructionPartTwo);
        }

        private long SolvePartOne(string[] input)
        {
            var instructionPartOne = input.Select(line => line.Split(' ')).Select(instructions => (instructions[0][0], long.Parse(instructions[1]))).ToList();

            return CalculateArea(instructionPartOne);
        }

        private static (char, long) ParseLine(string line)
        {
            //Data input format: R 6 (#70c710)
            int hex = line.IndexOf('#') + 1;
            long distance = Convert.ToInt64(line[hex..(hex + 5)], 16);
            char direction = line[hex + 5] switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                '3' => 'U'
            };

            return (direction, distance);
        }

        private static long CalculateArea(List<(char, long)> input)
        {
            Point point = new Point(0, 0);
            List<Point> points = [new Point(0, 0)];
            long perimeter = 0;

            foreach (var p in input)
            {
                long distance = p.Item2;
                point = p.Item1 switch
                {
                    'R' => point with { x = point.x + distance },
                    'L' => point with { x = point.x - distance },
                    'U' => point with { y = point.y + distance },
                    'D' => point with { y = point.y - distance },
                };

                perimeter += distance;
                points.Add(point);
            }

            //Assuming a non-intersecting polygon
            long area = 0;
            int j = points.Count - 1;
            for (int i = 0; i < points.Count; i++)
            {
                area += Determinant(points[i].x, points[i].y, points[j].x, points[j].y);
                j = i;
            }
            return (Math.Abs(area) + perimeter) / 2 + 1;
        }

        private static long Determinant(long x1, long y1, long x2, long y2)
        {
            return x1 * y2 - x2 * y1;
        }
    }
}
