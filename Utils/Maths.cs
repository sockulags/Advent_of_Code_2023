using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.HelperClass
    {
    public static class Maths
    {
        static long GreatestCommonFactor(long n1, long n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return GreatestCommonFactor(n2, n1 % n2);
            }
        }

        public static long LeastCommonMultiple(this List<long> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / GreatestCommonFactor(S, val));
        }

        public static long CalculateLagrange(long x, long[] values)
        {
            long term1 = ((x - 2) * (x - 4) / ((0 - 2) * (0 - 4))) * values[1];
            long term2 = ((x - 0) * (x - 4) / ((2 - 0) * (2 - 4))) * values[2];
            long term3 = ((x - 0) * (x - 2) / ((4 - 0) * (4 - 2))) * values[3];

            return term1 + term2 + term3;
        }
    }
}
