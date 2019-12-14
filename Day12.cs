using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    class Day12
    {
        public string CalcA()
        {
            var input = "<x=-3, y=15, z=-11>\n<x=3, y=13, z=-19>\n<x=-13, y=18, z=-2>\n<x=6, y=0, z=-1>";
            //var input = "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>";
            var satPos = new List<(int x, int y, int z)>();
            var satVelo = new List<(int dx, int dy, int dz)>();
            foreach (var inpu in input.Split('\n'))
            {
                var m = Regex.Match(inpu, "<x=(?<x>-?\\d+), y=(?<y>-?\\d+), z=(?<z>-?\\d+)>", RegexOptions.Singleline);
                int x = int.Parse(m.Groups["x"].Value);
                int y = int.Parse(m.Groups["y"].Value);
                int z = int.Parse(m.Groups["z"].Value);
                satPos.Add((x, y, z));
                satVelo.Add((0, 0, 0));
            }

            for (int step = 0; step < 1000; step++)
            {
                for (int sat1 = 0; sat1 < satPos.Count - 1; sat1++)
                for (int sat2 = sat1 + 1; sat2 < satPos.Count; sat2++)
                {
                    int dx = GetDeltaVal(satPos[sat1].x, satPos[sat2].x);
                    int dy = GetDeltaVal(satPos[sat1].y, satPos[sat2].y);
                    int dz = GetDeltaVal(satPos[sat1].z, satPos[sat2].z);
                    satVelo[sat1] = (satVelo[sat1].dx + dx, satVelo[sat1].dy + dy, satVelo[sat1].dz + dz);
                    satVelo[sat2] = (satVelo[sat2].dx - dx, satVelo[sat2].dy - dy, satVelo[sat2].dz - dz);
                }

                for (int sat1 = 0; sat1 < satPos.Count; sat1++)
                {
                    satPos[sat1] = (satPos[sat1].x + satVelo[sat1].dx, satPos[sat1].y + satVelo[sat1].dy, satPos[sat1].z + satVelo[sat1].dz);
                }
            }

            int totalEnergy = 0;
            for (int i = 0; i < satPos.Count; i++)
            {
                totalEnergy += GetEnergy(satPos[i]) * GetEnergy(satVelo[i]);
            }

            return totalEnergy.ToString();
        }

        public string CalcB()
        {
            var input = "<x=-3, y=15, z=-11>\n<x=3, y=13, z=-19>\n<x=-13, y=18, z=-2>\n<x=6, y=0, z=-1>";
            var satPos = new List<(int x, int y, int z)>();
            var satVelo = new List<(int dx, int dy, int dz)>();
            foreach (var inpu in input.Split('\n'))
            {
                var m = Regex.Match(inpu, "<x=(?<x>-?\\d+), y=(?<y>-?\\d+), z=(?<z>-?\\d+)>", RegexOptions.Singleline);
                int x = int.Parse(m.Groups["x"].Value);
                int y = int.Parse(m.Groups["y"].Value);
                int z = int.Parse(m.Groups["z"].Value);
                satPos.Add((x, y, z));
                satVelo.Add((0, 0, 0));
            }

            int xPeriod = FindPeriod(satPos, satVelo, (a, b) => (a.x, b.dx));
            int yPeriod = FindPeriod(satPos, satVelo, (a, b) => (a.y, b.dy));
            int zPeriod = FindPeriod(satPos, satVelo, (a, b) => (a.z, b.dz));

            int gcd = GCD(new[]{xPeriod, yPeriod, zPeriod});
            while (gcd > 1)
            {
                xPeriod /= gcd;
                yPeriod /= gcd;
                zPeriod /= gcd;
                gcd = GCD(new[] { xPeriod, yPeriod, zPeriod });
            }

            return ((long)xPeriod * yPeriod * zPeriod) .ToString();
        }// 4007229034815552 to big

        static int GCD(int[] numbers)
        {
            return numbers.Aggregate(GCD);
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private static int FindPeriod(List<(int x, int y, int z)> satPos, List<(int dx, int dy, int dz)> satVelo, Func<(int x, int y, int z), (int dx, int dy, int dz), (int a, int b)> func)
        {
            int iter = 0;
            var visited = new HashSet<((int a, int b) a, (int a, int b) b, (int a, int b) c, (int a, int b) d)>();
            while (true)
            {
                for (int sat1 = 0; sat1 < satPos.Count - 1; sat1++)
                for (int sat2 = sat1 + 1; sat2 < satPos.Count; sat2++)
                {
                    int dx = GetDeltaVal(satPos[sat1].x, satPos[sat2].x);
                    int dy = GetDeltaVal(satPos[sat1].y, satPos[sat2].y);
                    int dz = GetDeltaVal(satPos[sat1].z, satPos[sat2].z);
                    satVelo[sat1] = (satVelo[sat1].dx + dx, satVelo[sat1].dy + dy, satVelo[sat1].dz + dz);
                    satVelo[sat2] = (satVelo[sat2].dx - dx, satVelo[sat2].dy - dy, satVelo[sat2].dz - dz);
                }

                for (int sat1 = 0; sat1 < satPos.Count; sat1++)
                {
                    satPos[sat1] = (satPos[sat1].x + satVelo[sat1].dx, satPos[sat1].y + satVelo[sat1].dy,
                        satPos[sat1].z + satVelo[sat1].dz);
                }

                var state  = (func(satPos[0], satVelo[0]), func(satPos[1], satVelo[1]), func(satPos[2], satVelo[2]), func(satPos[3], satVelo[3]));
                if (visited.Add(state) == false)
                    return iter;

                iter++;
            }
        }

        private static int GetDeltaVal(int sat1, int sat2)
        {
            if (sat1 == sat2) return 0;
            return sat1 < sat2 ? 1 : -1;
        }

        private static int GetEnergy((int x, int y, int z) tup)
        {
            return Math.Abs(tup.x) + Math.Abs(tup.y) + Math.Abs(tup.z);
        }
    }
}