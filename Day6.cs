using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    class Day6
    {
        public string CalcA()
        {
            var orbits = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines("Day6Data.txt"))
            {
                var parts = line.Split(')');
                orbits.Add(parts[1], parts[0]);
            }

            int CountOrbist(string item)
            {
                int cnt = 0;
                while (true)
                {
                    if (item == "COM")
                        return cnt;
                    item = orbits[item];
                    cnt++;
                }
            }

            int total = 0;
            foreach (var o in orbits.Keys)
                total += CountOrbist(o);

            return total.ToString();
        }
        public string CalcB()
        {
            var orbits = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines("Day6Data.txt"))
            {
                var parts = line.Split(')');
                orbits.Add(parts[1], parts[0]);
            }

            var santaToCom = new Dictionary<string, int>();
            var orb = orbits["SAN"];
            int count = 0;
            while (orb != "COM")
            {
                santaToCom.Add(orb, count++);
                orb = orbits[orb];
            }

            count = 0;
            orb = orbits["YOU"];
            while (orb != "COM")
            {
                if (santaToCom.TryGetValue(orb, out var cnt))
                {
                    count += cnt;
                    break;
                }

                orb = orbits[orb];
                count++;
            }


            return count.ToString();
        }

    }
}
