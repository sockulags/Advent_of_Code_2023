using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023
{
    public class Day
    {
        public int Date { get; set; }
        public long Part1Score { get; set; }
        public long Part2Score { get; set; }
        public long TimeToSolve { get; set; }

        public override string ToString()
        {
            return Answer(Date, Part1Score, Part2Score, TimeToSolve);
        }

        public static string Answer(int date, long part1Score, long part2Score = 0, long timeToSolve = 0)
        {
            return $"[Day {date}] Part 1: {part1Score}, Part 2: {part2Score}, Time to solve: {timeToSolve}ms";
        }
    }

}

