using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace AdventOfCode2019
{
    class Day10
    {
        public string CalcA()
        {
            const string belt =
                @".............#..#.#......##........#..#
.#...##....#........##.#......#......#.
..#.#.#...#...#...##.#...#.............
.....##.................#.....##..#.#.#
......##...#.##......#..#.......#......
......#.....#....#.#..#..##....#.......
...................##.#..#.....#.....#.
#.....#.##.....#...##....#####....#.#..
..#.#..........#..##.......#.#...#....#
...#.#..#...#......#..........###.#....
##..##...#.#.......##....#.#..#...##...
..........#.#....#.#.#......#.....#....
....#.........#..#..##..#.##........#..
........#......###..............#.#....
...##.#...#.#.#......#........#........
......##.#.....#.#.....#..#.....#.#....
..#....#.###..#...##.#..##............#
...##..#...#.##.#.#....#.#.....#...#..#
......#............#.##..#..#....##....
.#.#.......#..#...###...........#.#.##.
........##........#.#...#.#......##....
.#.#........#......#..........#....#...
...............#...#........##..#.#....
.#......#....#.......#..#......#.......
.....#...#.#...#...#..###......#.##....
.#...#..##................##.#.........
..###...#.......#.##.#....#....#....#.#
...#..#.......###.............##.#.....
#..##....###.......##........#..#...#.#
.#......#...#...#.##......#..#.........
#...#.....#......#..##.............#...
...###.........###.###.#.....###.#.#...
#......#......#.#..#....#..#.....##.#..
.##....#.....#...#.##..#.#..##.......#.
..#........#.......##.##....#......#...
##............#....#.#.....#...........
........###.............##...#........#
#.........#.....#..##.#.#.#..#....#....
..............##.#.#.#...........#.....";
            var beltRows = belt.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int maxX = beltRows[0].Length;
            int maxY = beltRows.Length;

            int bestVal = int.MinValue;
            int bestX, bestY;

            for (int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++)
                {
                    if (beltRows[y][x] != '#')
                        continue;

                    var equations = new HashSet<(double k, double m, bool posX, bool posY)>();

                    for (int y2 = 0; y2 < maxY; y2++)
                        for (int x2 = 0; x2 < maxX; x2++)
                        {
                            if (y2 == y && x2 == x)
                                continue;
                            if (beltRows[y2][x2] != '#')
                                continue;

                            var k = (y2 - (double)y) / (x2 - (double)x);
                            var m = y - k * x;
                            if (double.IsInfinity(k) || double.IsNaN(k))
                            {
                                m = y;
                                k = double.PositiveInfinity;
                            }
                            equations.Add((Round(k, 12), Round(m, 12), x2 > x, y2 > y));
                        }


                    if (equations.Count > bestVal)
                    {
                        bestVal = equations.Count;
                        bestX = x;
                        bestY = y;
                    }
                }

            return bestVal.ToString();
        }

        public string CalcB()
        {
            //                const string belt =
            //                    @".#..##.###...#######
            //##.############..##.
            //.#.######.########.#
            //.###.#######.####.#.
            //#####.##.#.##.###.##
            //..#####..#.#########
            //####################
            //#.####....###.#.#.##
            //##.#################
            //#####.##.###..####..
            //..######..##.#######
            //####.##.####...##..#
            //.#####..#.######.###
            //##...#.##########...
            //#.##########.#######
            //.####.#.###.###.#.##
            //....##.##.###..#####
            //.#.#.###########.###
            //#.#.#.#####.####.###
            //###.##.####.##.#..##";
            const string belt =
                @".............#..#.#......##........#..#
.#...##....#........##.#......#......#.
..#.#.#...#...#...##.#...#.............
.....##.................#.....##..#.#.#
......##...#.##......#..#.......#......
......#.....#....#.#..#..##....#.......
...................##.#..#.....#.....#.
#.....#.##.....#...##....#####....#.#..
..#.#..........#..##.......#.#...#....#
...#.#..#...#......#..........###.#....
##..##...#.#.......##....#.#..#...##...
..........#.#....#.#.#......#.....#....
....#.........#..#..##..#.##........#..
........#......###..............#.#....
...##.#...#.#.#......#........#........
......##.#.....#.#.....#..#.....#.#....
..#....#.###..#...##.#..##............#
...##..#...#.##.#.#....#.#.....#...#..#
......#............#.##..#..#....##....
.#.#.......#..#...###...........#.#.##.
........##........#.#...#.#......##....
.#.#........#......#..........#....#...
...............#...#........##..#.#....
.#......#....#.......#..#......#.......
.....#...#.#...#...#..###......#.##....
.#...#..##................##.#.........
..###...#.......#.##.#....#....#....#.#
...#..#.......###.............##.#.....
#..##....###.......##........#..#...#.#
.#......#...#...#.##......#..#.........
#...#.....#......#..##.............#...
...###.........###.###.#.....###.#.#...
#......#......#.#..#....#..#.....##.#..
.##....#.....#...#.##..#.#..##.......#.
..#........#.......##.##....#......#...
##............#....#.#.....#...........
........###.............##...#........#
#.........#.....#..##.#.#.#..#....#....
..............##.#.#.#...........#.....";


            var beltRows = belt.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int maxX = beltRows[0].Length;
            int maxY = beltRows.Length;

            const int x = 26;
            const int y = 29;
            if (beltRows[y][x] != '#')
                throw new Exception("Knas!");

            var equations = new Dictionary<(double k, bool posX, bool posY), List<(int x, int y)>>();

            for (int y2 = 0; y2 < maxY; y2++)
                for (int x2 = 0; x2 < maxX; x2++)
                {
                    if (y2 == y && x2 == x)
                        continue;
                    if (beltRows[y2][x2] != '#')
                        continue;

                    var k = (y2 - (double)y) / (x2 - (double)x);
                    if (double.IsInfinity(k) || double.IsNaN(k))
                    {
                        k = double.PositiveInfinity;
                    }

                    var key = (Round(k, 10), x2 > x, y2 > y);
                    if (equations.ContainsKey(key))
                    {
                        equations[key].Add((x2, y2));
                    }
                    else
                    {
                        equations[key] = new List<(int, int)> { (x2, y2) };
                    }
                }


            var removed = new List<(int x, int y)>();
            while (equations.Count > 0)
            {
                // Straigt up
                if (equations.ContainsKey((double.PositiveInfinity, false, false)))
                {
                    RemoveClosest(equations, (double.PositiveInfinity, false, false), x, y, removed);
                }

                // From up to right
                foreach (var dir in equations.Keys.Where(k => k.posX && !k.posY && double.IsInfinity(k.k) == false).OrderBy(k => k.k))
                {
                    RemoveClosest(equations, dir, x, y, removed);
                }

                // From right to down
                foreach (var dir in equations.Keys.Where(k => k.posX && k.posY && double.IsInfinity(k.k) == false).OrderBy(k => k.k))
                {
                    RemoveClosest(equations, dir, x, y, removed);
                }

                // Down
                if (equations.ContainsKey((double.PositiveInfinity, false, true)))
                {
                    RemoveClosest(equations, (double.PositiveInfinity, false, true), x, y, removed);
                }

                // Down to left
                foreach (var dir in equations.Keys.Where(k => !k.posX && k.posY && double.IsInfinity(k.k) == false).OrderBy(k => k.k))
                {
                    RemoveClosest(equations, dir, x, y, removed);
                }

                // Left to up
                foreach (var dir in equations.Keys.Where(k => !k.posX && !k.posY && double.IsInfinity(k.k) == false).OrderBy(k => k.k))
                {
                    RemoveClosest(equations, dir, x, y, removed);
                }
            }


            return (removed[199].x * 100 + removed[199].y).ToString();
        }

        private static (int x, int y) RemoveClosest(List<(int x, int y)> list, int x2, int y2)
        {
            var closest = list.OrderBy(p => Abs(p.x - x2) + Abs(p.y - y2)).First();
            list.Remove(closest);
            return closest;
        }

        private static void RemoveClosest(Dictionary<(double k, bool posX, bool posY), List<(int x, int y)>> dict, (double k, bool posX, bool posY) key, int x2, int y2, List<(int x, int y)> removed)
        {
            var list = dict[key];
            var ret = RemoveClosest(list, x2, y2);
            removed.Add(ret);

            if (list.Count == 0)
                dict.Remove(key);
        }
    }
}
