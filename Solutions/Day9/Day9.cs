using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day9
    {
        static readonly int Date = 9;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            long p1 = 0;
            long p2 = 0;
            string[] input = FileHelper.ReadInput(InputSource);
            foreach (string line in input)
            {

                string[] numbers = line.Split(' ');
                long[] data = new long[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    data[i] = long.Parse(numbers[i]);
                }
                p1 += ExtrapolatedNumbers(data);
                p2 += ExtrapolatedNumbers(data, 2);
            }
            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private long ExtrapolatedNumbers(long[] data, int part = 1)
        {
            while (!data.All(x => x == 0))
            {
                long[] newArr = new long[data.Length - 1];
                for (int i = 0; i < newArr.Length; i++)
                {
                    newArr[i] = data[i + 1] - data[i];
                }
                if (part == 1)
                    return data[^1] + ExtrapolatedNumbers(newArr);
                else
                    return data[0] - ExtrapolatedNumbers(newArr, 2);
            }

            return part == 1 ? data[^1] : data[0];
        }


    }
}
