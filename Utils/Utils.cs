using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.HelperClass
    {
    public static class Utils
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
    }
}
