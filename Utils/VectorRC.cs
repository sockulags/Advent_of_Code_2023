﻿namespace Advent_of_Code_2023.HelperClass
{
    public record struct VectorRC(int Row, int Col)
    {
        public static readonly VectorRC Zero = new VectorRC(0, 0);
        public static readonly VectorRC Up = new VectorRC(-1, 0);
        public static readonly VectorRC Down = new VectorRC(+1, 0);
        public static readonly VectorRC Left = new VectorRC(0, -1);
        public static readonly VectorRC Right = new VectorRC(0, +1);

        public static VectorRC operator +(VectorRC left, VectorRC right)
        {
            return new VectorRC(left.Row + right.Row, left.Col + right.Col);
        }
        public static VectorRC operator -(VectorRC left, VectorRC right)
        {
            return new VectorRC(left.Row - right.Row, left.Col - right.Col);
        }
        public static VectorRC operator -(VectorRC val)
        {
            return new VectorRC(-val.Row, -val.Col);
        }

        public readonly int Dot(VectorRC that)
        {
            return this.Row * that.Row + this.Col * that.Col;
        }
        public readonly VectorRC RotatedLeft()
        {
            return new VectorRC(-Col, Row);
        }
        public readonly VectorRC RotatedRight()
        {
            return new VectorRC(Col, -Row);
        }

        public readonly int ManhattanMetric()
        {
            return Math.Abs(Row) + Math.Abs(Col);
        }
        public readonly int ChebyshevMetric()
        {
            return Math.Max(Math.Abs(Row), Math.Abs(Col));
        }

        public readonly VectorRC NextUp()
        {
            return this + Up;
        }
        public readonly VectorRC NextDown()
        {
            return this + Down;
        }
        public readonly VectorRC NextLeft()
        {
            return this + Left;
        }
        public readonly VectorRC NextRight()
        {
            return this + Right;
        }
        public readonly VectorRC[] NextFour()
        {
            return
                [
                    this + Up,
                    this + Down,
                    this + Left,
                    this + Right,
                ];
        }
        public readonly VectorRC[] NextEight()
        {
            return
                [
                    this + Up + Left,
                    this + Up,
                    this + Up + Right,
                    this + Left,
                    this + Right,
                    this + Down + Left,
                    this + Down,
                    this + Down + Right,
                ];
        }
    }
}
