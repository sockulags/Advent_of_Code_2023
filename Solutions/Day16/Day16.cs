using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day16
    {
        static readonly int Date = 16;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            char[,] field = GetField(input);

            long resultPartOne = Solver(field, Tuple.Create(0, 0), Directions.East);

            long resultPartTwo = TryAllStartingSpots(field);
            return Day.Answer(Date, resultPartOne, resultPartTwo, sw.ElapsedMilliseconds);
        }

        private char[,] GetField(string[] input)
        {
            char[,] field = new char[input.Length, input[0].Length];
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    field[x, y] = input[x][y];
                }
            }

            return field;
        }

        private long TryAllStartingSpots(char[,] field)
        {
            long result = 0;
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    long temp = 0;
                    long temp2 = 0;
                    if (y == 0)
                    {
                        temp = Solver(field, Tuple.Create(y, x), Directions.North);
                    }
                    else if (y == field.GetLength(0) - 1)
                    {

                        temp = Solver(field, Tuple.Create(y, x), Directions.South);
                    }
                    if (x == 0)
                    {
                        temp2 = Solver(field, Tuple.Create(y, x), Directions.East);
                    }
                    else if (x == field.GetLength(1) - 1)
                    {
                        temp2 = Solver(field, Tuple.Create(y, x), Directions.West);
                    }

                    temp = temp2 > temp ? temp2 : temp;
                    result = temp > result ? temp : result;
                }
            }

            return result;
        }

        private long Solver(char[,] field, Tuple<int, int> start, Directions d)
        {

            List<Tuple<int, int>> altPaths = new() { start };

            int[,] energized = new int[field.GetLength(0), field.GetLength(1)];
            Directions direction = d;
            List<Directions> dirs = [direction];

            for (int i = 0; i < altPaths.Count; i++)
            {
                bool outOfBounds = false;
                direction = dirs[i];
                int x = altPaths[i].Item2, y = altPaths[i].Item1;
                while (!outOfBounds)
                {
                    if (x < 0 || y < 0 || x >= field.GetLength(1) || y >= field.GetLength(0))
                    {
                        outOfBounds = true; break;
                    }
                    else
                    {
                        energized[y, x] = 1;
                    }
                    switch (field[y, x])
                    {
                        case '|' when direction == Directions.North:
                        case '.' when direction == Directions.North:
                            y += 1;
                            break;
                        case '.' when direction == Directions.South:
                        case '|' when direction == Directions.South:
                            y -= 1;
                            break;
                        case '.' when direction == Directions.West:
                        case '-' when direction == Directions.West:
                            x -= 1;
                            break;
                        case '.' when direction == Directions.East:
                        case '-' when direction == Directions.East:
                            x += 1;
                            break;
                        case '|' when direction == Directions.East || direction == Directions.West:
                            var newTuple = Tuple.Create(y + 1, x);
                            bool containsValue = altPaths.Contains(newTuple, new TupleEqualityComparer());
                            if (!containsValue)
                            {
                                altPaths.Add(newTuple);
                                dirs.Add(Directions.North);
                                y -= 1;
                                direction = Directions.South;
                            }
                            else
                            {
                                outOfBounds = true;
                            }
                            break;
                        case '-' when direction == Directions.North || direction == Directions.South:
                            var test = Tuple.Create(y, x + 1);
                            containsValue = altPaths.Contains(test, new TupleEqualityComparer());
                            if (!containsValue)
                            {
                                altPaths.Add(new(y, x + 1));
                                dirs.Add(Directions.East);
                                x -= 1;
                                direction = Directions.West;
                            }
                            else { outOfBounds = true; }
                            break;
                        case '/' when direction == Directions.East:
                        case '\\' when direction == Directions.West:
                            direction = Directions.South;
                            y -= 1;
                            break;
                        case '/' when direction == Directions.West:
                        case '\\' when direction == Directions.East:
                            direction = Directions.North;
                            y += 1;
                            break;
                        case '/' when direction == Directions.North:
                        case '\\' when direction == Directions.South:
                            direction = Directions.West;
                            x -= 1;
                            break;
                        case '/' when direction == Directions.South:
                        case '\\' when direction == Directions.North:
                            direction = Directions.East;
                            x += 1;
                            break;
                        default:
                            break;

                    }
                }

            }
            int sum = 0;
            sum = GetEnergizedSpots(energized, sum);

            return sum;

        }

        private int GetEnergizedSpots(int[,] energized, int sum)
        {
            for (int row = 0; row < energized.GetLength(0); row++)
            {
                for (int col = 0; col < energized.GetLength(1); col++)
                {
                    // Add the current element to the sum
                    sum += energized[row, col];

                }

            }

            return sum;
        }
    }
}
