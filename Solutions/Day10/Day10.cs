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
    public class Day10
    {
        static readonly int Date = 10;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
           input = FileHelper.ReadInput(InputSource);
            int startRow = Array.FindIndex(input, x => x.Contains('S'));
            int startCol = input.First(x => x.Contains('S')).IndexOf('S');
            int[] pos = new int[] { startRow, startCol };

            map = new int[input.Length, input[0].Length];
            previous[0] = startRow;
            previous[1] = startCol;
            map[startRow, startCol] = 1;

            while (sCount > 0)
            {
                pos = FullPath(pos[0], pos[1]);
            }

            long p1 = pathLength / 2;
            long p2 = SearchMap();

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        static int pathLength = -1;
        static string[] input;
        static int[] previous = new int[2];
        static int[,] map;
        static int sCount = 2;


        private static int SearchMap()
        {
            List<string> newMap = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (map[i, j] == 0)
                        input[i] = UpdateString(input[i], j);
                    sb.Append(input[i][j]);
                }
                newMap.Add(Regex.Replace(Regex.Replace(sb.ToString(), "F-*7|L-*J", string.Empty), "F-*J|L-*7", "|"));
            }
            int ans = 0;

            foreach (var l in newMap)
            {
                int parity = 0;
                foreach (var c in l)
                {
                    if (c == '|') parity++;
                    if (c == '.' && parity % 2 == 1) ans++;
                }
            }
            return ans;
        }

        static int[] FullPath(int row, int col)
        {
            pathLength++;
            int newRow = 0;
            int newCol = 0;

            if (input[row][col] == 'S')
            {
                sCount--;
                if (North(row == 0 ? '3' : input[row - 1][col]))
                {
                    newRow = row - 1;
                    newCol = col;
                }

                else if (East(col == input[row].Length - 1 ? '3' : input[row][col + 1]))
                {
                    newRow = row;
                    newCol = col + 1;
                }
                else if (West(col == 0 ? '3' : input[row][col - 1]))
                {
                    newRow = row;
                    newCol = col - 1;
                }
                else if (South(row == input.Length - 1 ? '3' : input[row + 1][col]))
                {
                    newRow = row + 1;
                    newCol = col;
                }

            }
            else if (input[row][col] != 'S')
            {
                switch (input[row][col])
                {
                    case '|':
                        newRow = row + 1 == previous[0] ? row - 1 : row + 1;
                        newCol = col;
                        break;
                    case '-':
                        newRow = row;
                        newCol = previous[1] == col - 1 ? col + 1 : col - 1;
                        break;
                    case 'L':
                        if (previous[1] == col + 1)
                        {
                            newRow = row - 1;
                            newCol = col;
                        }
                        else
                        {
                            newRow = row;
                            newCol = col + 1;
                        }
                        break;
                    case 'J':
                        if (previous[0] == row - 1)
                        {
                            newRow = row;
                            newCol = col - 1;
                        }
                        else
                        {
                            newRow = row - 1;
                            newCol = col;
                        }
                        break;
                    case '7':
                        if (previous[0] == row + 1)
                        {
                            newRow = row;
                            newCol = col - 1;
                        }
                        else
                        {
                            newRow = row + 1;
                            newCol = col;
                        }
                        break;

                    case 'F':
                        if (previous[1] == col + 1)
                        {
                            newRow = row + 1;
                            newCol = col;
                        }
                        else
                        {
                            newRow = row;
                            newCol = col + 1;
                        }
                        break;
                }
            }
            previous[0] = row;
            previous[1] = col;
            map[newRow, newCol] = 1;
            return new int[] { newRow, newCol };
        }

        private static string UpdateString(string v, int col)
        {
            char[] chars = v.ToCharArray();
            chars[col] = '.';
            return new string(chars);
        }

        private static bool North(char v) => v == '|' || v == 'F' || v == '7';
        private static bool East(char v) => v == '-' || v == 'J' || v == '7';
        private static bool West(char v) => v == '-' || v == 'L' || v == 'F';
        private static bool South(char v) => v == '|' || v == 'L' || v == 'J';



    }
}
