using SockulagsClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2023.Solutions
{
    public class Day15
    {
        static readonly int Date = 15;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = SolvePartOne(input);
            long p2 = SolvePartTwo(input);

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }

        private long SolvePartTwo(string[] input)
        {
            long resultPartTwo = 0;
            Dictionary<long, List<string>> hashmap = new();
            foreach (string line in input)
            {
                string[] ascii = line.Split(',');
                foreach (string word in ascii)
                {
                    long wordVal = 0;
                    int charPos = 0;
                    foreach (char c in word)
                    {
                        if (c == '-' && hashmap.ContainsKey(wordVal))
                        {
                            var x = hashmap[wordVal].Where(x => x.StartsWith(word[..charPos])).FirstOrDefault();
                            if (x != null)
                                hashmap[wordVal].Remove(x);
                            break;
                        }
                        else if (c == '=')
                        {
                            if (!hashmap.ContainsKey(wordVal))
                            {
                                hashmap.Add(wordVal, new List<string>() { word });
                            }
                            else
                            {
                                var x = hashmap[wordVal].Where(x => x.StartsWith(word[..charPos])).FirstOrDefault();
                                if (x != null)
                                {
                                    var i = hashmap[wordVal].IndexOf(x);
                                    hashmap[wordVal][i] = word;
                                }
                                else
                                {
                                    hashmap[wordVal].Add(word);

                                }
                            }
                            break;
                        }

                        wordVal += (long)c;
                        wordVal *= 17;
                        wordVal = wordVal % 256;
                        charPos++;
                    }

                    resultPartTwo += wordVal;
                }
                resultPartTwo = FocusingPower(hashmap);
            }

            return resultPartTwo;
        }

        private long FocusingPower(Dictionary<long, List<string>> hashmap)
        {
            long result = 0;
            foreach (var x in hashmap.Keys)
            {
                if (hashmap[x].Count > 0)
                {
                    long slotPos = 1;

                    foreach (var item in hashmap[x])
                    {
                        char c = item[^1];
                        result += (x + 1) * slotPos++ * long.Parse(c.ToString());
                    }
                    slotPos = 0;
                }

            }
            return result;
        }

        private long SolvePartOne(string[] input)
        {
            long resultPartOne = 0;
            foreach (string line in input)
            {
                string[] ascii = line.Split(',');
                foreach (string word in ascii)
                {
                    long wordVal = 0;
                    foreach (char c in word)
                    {
                        wordVal += (long)c;
                        wordVal *= 17;
                        wordVal = wordVal % 256;
                    }

                    resultPartOne += wordVal;
                }
            }
            return resultPartOne;
        }
    }
}
