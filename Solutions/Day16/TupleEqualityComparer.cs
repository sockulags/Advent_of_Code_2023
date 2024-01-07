using System.Diagnostics.CodeAnalysis;

namespace Advent_of_Code_2023.Solutions
{
    internal class TupleEqualityComparer : IEqualityComparer<Tuple<int, int>>
    {
        public bool Equals(Tuple<int, int>? x, Tuple<int, int>? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Item1 == y.Item1 && x.Item2 == y.Item2;
        }

        public int GetHashCode([DisallowNull] Tuple<int, int> obj)
        {
            throw new NotImplementedException();
        }
    }
}
