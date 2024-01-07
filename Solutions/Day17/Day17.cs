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
    public class Day17
    {
        static readonly int Date = 17;
       static string InputSource = $"Day{Date}";

        private static readonly Grid map = new Grid(FileHelper.ReadInput(InputSource));

        private static readonly char OUTSIDE = '\0';

        public static int maxDir = 0;
        private record Node(VectorRC Position, VectorRC Direction);

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);

            Node start = new(VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            maxDir = 3;
            long p1 = GraphAlgos.DijkstraToEnd(start, GetNeighbours, node => node.Position == end).distance;
            maxDir = 10;
            long p2 = GraphAlgos.DijkstraToEnd(start, GetNeighbours, node => node.Position == end).distance;

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private IEnumerable<(Node, int)> GetNeighbours(Node node)
        {
            VectorRC[] nextDirections = node.Direction == VectorRC.Zero ? [VectorRC.Right, VectorRC.Down] : [node.Direction.RotatedLeft(), node.Direction.RotatedRight()];
            foreach (var dir in nextDirections)
            {
                var nextPos = node.Position;
                int cost = 0;
                for (int i = 0; i < maxDir; i++)
                {
                    nextPos += dir;
                    char nextTile = map.Get(nextPos);
                    if (nextTile == OUTSIDE)
                    {
                        break;
                    }
                    cost += nextTile - '0';
                    if (maxDir == 3)
                    {
                        yield return (new Node(nextPos, dir), cost);
                    }
                    else if (i >= 3)
                    {
                        yield return (new Node(nextPos, dir), cost);
                    }
                }
            }
        }
    }
}
