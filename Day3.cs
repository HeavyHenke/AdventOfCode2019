using System;
using System.Collections.Generic;
using System.IO;
using static System.Math;

namespace AdventOfCode2019
{
    class Day3
    {
        public string CalcA()
        {
            var lines = File.ReadAllLines("Day3Data.txt");
            var visited = new HashSet<(int x, int y)>();
            int x = 0, y = 0;

            var moves = new Dictionary<char, Action>
            {
                {'U', () => y--},
                {'D', () => y++},
                {'L', () => x--},
                {'R', () => x++}
            };


            var commands1 = lines[0].Split(',');
            foreach (var cmd in commands1)
            {
                var action = moves[cmd[0]];
                var count = int.Parse(cmd.Substring(1));
                for (int i = 0; i < count; i++)
                {
                    action();
                    visited.Add((x, y));
                }
            }

            x = y = 0;
            int minDist = int.MaxValue;
            var commands2 = lines[1].Split(',');
            foreach (var cmd in commands2)
            {
                var action = moves[cmd[0]];
                var count = int.Parse(cmd.Substring(1));
                for (int i = 0; i < count; i++)
                {
                    action();
                    if (visited.Contains((x, y)))
                    {
                        var dist = Abs(x) + Abs(y);
                        minDist = Min(minDist, dist);
                    }
                }
            }


            return minDist.ToString();
        }

        public string CalcB()
        {
            var lines = File.ReadAllLines("Day3Data.txt");
            var visited = new Dictionary<(int x, int y), int>();
            int x = 0, y = 0;

            var moves = new Dictionary<char, Action>
            {
                {'U', () => y--},
                {'D', () => y++},
                {'L', () => x--},
                {'R', () => x++}
            };

            int steps = 0;
            var commands1 = lines[0].Split(',');
            foreach (var cmd in commands1)
            {
                var action = moves[cmd[0]];
                var count = int.Parse(cmd.Substring(1));
                for (int i = 0; i < count; i++)
                {
                    action();
                    steps++;
                    if (visited.ContainsKey((x, y)) == false)
                        visited.Add((x, y), steps);
                }
            }

            steps = x = y = 0;
            int minDist = int.MaxValue;
            var commands2 = lines[1].Split(',');
            foreach (var cmd in commands2)
            {
                var action = moves[cmd[0]];
                var count = int.Parse(cmd.Substring(1));
                for (int i = 0; i < count; i++)
                {
                    action();
                    steps++;
                    if (visited.TryGetValue((x, y), out var otherWireSteps))
                    {
                        var dist = steps + otherWireSteps;
                        minDist = Min(minDist, dist);
                    }
                }
            }

            return minDist.ToString();
        }

    }
}