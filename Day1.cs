using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    class Day1
    {
        public string CalcA()
        {
            var fuels = File.ReadAllLines("Day1Data.txt")
                .Select(int.Parse)
                .Select(f => f / 3)
                .Select(f => f - 2)
                .Sum();


            return fuels.ToString();
        }

        public string CalcB()
        {
            var fuels = File.ReadAllLines("Day1Data.txt")
                .Select(int.Parse)
                .Select(GetTotalFuel)
                .Sum();


            return fuels.ToString();
        }

        private int GetTotalFuel(int moduleMass)
        {
            int total = 0;
            int mass = moduleMass;
            int delta;

            do
            {
                delta = mass / 3 - 2;
                if (delta < 0)
                    delta = 0;
                total += delta;
                mass = delta;

            } while (delta > 0);

            return total;
        }
    }
}