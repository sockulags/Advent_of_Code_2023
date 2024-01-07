using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent_of_Code_2023.HelperClass;

namespace Advent_of_Code_2023.Solutions
{

    public class Day21
    {
        public record Point(int x, int y);
        public static Point North = new Point(0, 1);
        public static Point West = new Point(-1, 0);
        public static Point East = new Point(1, 0);
        public static Point South = new Point(0, -1);

        public static Point[] direction = { North, East, South, West };

        static readonly int Date = 21;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();

            int size = 11;
            int start = (int)(Math.Pow(size, 2) / 2);

            var input = MapTo2dArray(FileHelper.ReadAllLines(InputSource).Select(x => x.ToCharArray()), size, start + 1);

            int partOne = 64;
            int partTwo = 26501365;
            long n = partTwo / 131;

            int[] breakpoints = [64, 65, 2 * 131 + 65, 4 * 131 + 65];
            //Find original and difference between first map overlap
            List<long> results = ReachedGardenPlots(input, breakpoints.Max(), breakpoints);

            long p1 = results[0];
            long p2 = Maths.CalculateLagrange((partTwo - 65) / 131, results.ToArray());

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private List<long> ReachedGardenPlots(char[,] input, int steps, int[] breakpoints)
        {
            List<(int, int)> visited = new();
            List<long> results = new();
            Queue<(Point, int)> queue = new Queue<(Point, int)>();

            int xMax = input.GetLength(0);
            int yMax = input.GetLength(1);

            Point start = input.Cast<char>()
                .Select((c, index) => new { Index = index, Char = c })
                .Where(item => item.Char == 'T')
                .Select(item => new Point(item.Index / input.GetLength(1), item.Index % input.GetLength(1)))
                .First();
            queue.Enqueue((start, steps));
            long possibleSteps = 0;
            long duplicateSteps = 0;
            int b = 0;
            while (queue.Count(x => x.Item2 > 0) > 0)
            {

                var curr = queue.Dequeue();
                for (int i = 0; i < direction.Length; i++)
                {

                    if (TryMove(input, curr.Item1, direction[i], visited) && curr.Item2 > 0)
                    {
                        queue.Enqueue((new Point(curr.Item1.x + direction[i].x, curr.Item1.y + direction[i].y), curr.Item2 - 1));
                        if (curr.Item2 % 2 != 0 && !visited.Contains((curr.Item1.x + direction[i].x, curr.Item1.y + direction[i].y)))
                        {
                            possibleSteps++;
                            visited.Add((curr.Item1.x + direction[i].x, curr.Item1.y + direction[i].y));
                        }
                        else if (curr.Item2 % 2 == 0 && !visited.Contains((curr.Item1.x + direction[i].x, curr.Item1.y + direction[i].y)))
                        {
                            duplicateSteps++;
                            visited.Add((curr.Item1.x + direction[i].x, curr.Item1.y + direction[i].y));
                        }
                    }
                }
                if (queue.Count(x => x.Item2 > steps - breakpoints[b]) == 0)
                {
                    if ((steps - breakpoints[b]) % 2 == 0)
                    {
                        results.Add(possibleSteps);
                    }
                    else
                    {
                        results.Add(duplicateSteps);
                    }
                    b++;
                }
            }

            return results;
        }

        private bool InBounds(Point curr, Point next, int xMax, int yMax)
        {
            return curr.x + next.x < xMax && curr.x + next.x >= 0 && curr.y + next.y < yMax && curr.y + next.y >= 0;
        }

        private bool TryMove(char[,] input, Point p, Point dir, List<(int, int)> visited)
        {
            return input[p.x + dir.x, p.y + dir.y] != '#' && !visited.Contains((p.x + dir.x, p.y + dir.y));
        }

        private char[,] MapTo2dArray(IEnumerable<char[]> charArrays, int size, int start)
        {
            char[,] charMatrix = new char[charArrays.Count() * size, charArrays.Max(a => a.Length) * size];
            int sCount = 0;
            for (int i = 0; i < charArrays.Count() * size; i++)
            {
                for (int j = 0; j < charArrays.Max(a => a.Length) * size; j++)
                {
                    charMatrix[i, j] = charArrays.ElementAt(i % charArrays.Count())[j % charArrays.Max(a => a.Length)];
                    if (charMatrix[i, j] == 'S')
                    {
                        sCount++;
                        if (sCount == start)
                        {
                            charMatrix[i, j] = 'T';
                        }
                    }

                }

            }
            return charMatrix;
        }
    }
}
