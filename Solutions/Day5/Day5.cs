using SockulagsClassLibrary;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Advent_of_Code_2023.Solutions
{
    public class Day5
    {
        static readonly int Date = 5;
        string InputSource = $"Day{Date}";

        public override string ToString()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] input = FileHelper.ReadInput(InputSource);
            long p1 = SolvePartOne(input);
            long p2 = SolvePartTwo(input);

            return Day.Answer(Date, p1, p2, sw.ElapsedMilliseconds);
        }
        List<long> Seeds = new List<long>();
        List<Path> SeedToSoil = new List<Path>();
        List<Path> SoilToFert = new List<Path>();
        List<Path> FertToWater = new List<Path>();
        List<Path> WaterToLight = new List<Path>();
        List<Path> LightToTemp = new List<Path>();
        List<Path> TempToHum = new List<Path>();
        List<Path> HumToLoc = new List<Path>();

        List<List<Path>> Path = new List<List<Path>>();
        long shortestPath = 9223372036854775807;
        int stage = 0;
        int iteration = 0;
        long range = 0;
        long startPos = long.MaxValue;
        long prev = 0;
        Dictionary<long, long> starts = new();
        private long SolvePartOne(string[] input)
        {
            Path.Add(SeedToSoil);
            Path.Add(SoilToFert);
            Path.Add(FertToWater);
            Path.Add(WaterToLight);
            Path.Add(LightToTemp);
            Path.Add(TempToHum);
            Path.Add(HumToLoc);
            Regex rx = new Regex(@"\d*");

            //Get Seeds
            foreach (Match match in rx.Matches(input[0]))
            {
                if (match.Length > 1)
                    Seeds.Add(long.Parse(match.Value));
            }

            //Get Paths
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i].Length > 0 && char.IsLetter(input[i][0]))
                {
                    stage++;
                }
                else if (input[i].Length >= 1)
                {
                    AddPathData(input[i], stage);
                }
            }
            foreach (var seed in Seeds)
            {

                long currPath = seed;

                for (int i = 0; i < Path.Count(); i++)
                {
                    for (int j = 0; j < Path[i].Count(); j++)
                    {
                        var val = Path[i][j];
                        if (currPath >= val.SourceValue && currPath <= val.RangeValue + val.SourceValue)
                        {
                            currPath = currPath + (val.StartValue - val.SourceValue);
                            break;
                        }
                    }
                }
                shortestPath = currPath < shortestPath ? currPath : shortestPath;
            }
            return shortestPath;
        }
        private long SolvePartTwo(string[] input)
        {
            for (int s = 0; s < Seeds.Count(); s += 2)
            {
                if (range <= Seeds[s] + Seeds[s + 1])
                {
                    range = Seeds[s] + Seeds[s + 1];
                }

                if (startPos > Seeds[s])
                {
                    startPos = Seeds[s];
                }
            }
            long interval = (range - startPos) / 100000;
            GetStartLocation(startPos, range, interval);

            return shortestPath;
        }

        private void AddPathData(string input, int stage)
        {
            string[] data = input.Split(" ");
            List<long> parsedData = new List<long>();
            foreach (string s in data)
            {
                if (s.Length > 0)
                {
                    parsedData.Add(long.Parse(s));
                }

            }
            switch (stage)
            {
                case 1:
                    SeedToSoil.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 2:
                    SoilToFert.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 3:
                    FertToWater.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 4:
                    WaterToLight.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 5:
                    LightToTemp.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 6:
                    TempToHum.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;
                case 7:
                    HumToLoc.Add(new Path()
                    {
                        StartValue = parsedData[0],
                        SourceValue = parsedData[1],
                        RangeValue = parsedData[2]
                    });
                    break;


            }
        }

        private void GetStartLocation(long start, long end, long interval)
        {
            startPos = start;
            range = end;
            for (long r = range - 1; r >= startPos; r -= interval)
            {
                long currPath = r;
                for (int i = 0; i < Path.Count(); i++)
                {
                    for (int j = 0; j < Path[i].Count(); j++)
                    {
                        var val = Path[i][j];
                        if (currPath >= val.SourceValue && currPath <= val.RangeValue + val.SourceValue - 1)
                        {
                            currPath = currPath + (val.StartValue - val.SourceValue);
                            break;

                        }
                    }

                }
                for (int s = 0; s < Seeds.Count(); s += 2)
                {
                    if (r >= Seeds[s] && r <= Seeds[s] + Seeds[s + 1])
                    {
                        if (currPath <= shortestPath)
                        {
                            shortestPath = currPath;
                            
                            if (prev > 1 && char.Equals(prev.ToString()[..8], r.ToString()[..8]))
                            {
                                startPos = r - r / 100000;
                                range = r + r / 100000;

                            }
                            else if (prev > 1 && char.Equals(prev.ToString()[..7], r.ToString()[..7]))
                            {
                                startPos = r - r / 1000000;
                                range = r + r / 1000000;

                            }
                            else if (prev > 1 && char.Equals(prev.ToString()[..6], r.ToString()[..6]))
                            {
                                startPos = r - r / 100000;
                                range = r + r / 100000;

                            }
                            prev = r;
                        }
                    }
                }
            }


            if (interval > 1)
            {
                GetStartLocation(startPos, range, interval / 2);
            }

        }
    }

    public class Path
    {
        public long StartValue { get; set; }
        public long SourceValue { get; set; }
        public long RangeValue { get; set; }
    }
}
