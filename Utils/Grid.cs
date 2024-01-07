using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.HelperClass
{
    public class Grid
    {
        public ImmutableArray<string> Data { get; set; }
        public int Height { get => Data.Length; }
        public int Width { get; set; }

        public Grid(string[] input)
        {
            Data = input.ToImmutableArray();
            Width = Data.Max(row => row.Length);
        }

        public char Get(int row, int col)
        {
            if (row < 0 || row >= Data.Length || col < 0 || col >= Data[row].Length)
            {
                return '\0';
            }
            return Data[row][col];
        }

        public char Get(VectorRC coord)
        {
            return Get(coord.Row, coord.Col);
        }




    }
}
