using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    class Day14
    {
        public double CalcA()
        {
            return CalcAWithParam(1);
        }

        private static double CalcAWithParam(double neededFuel)
        {
            var formulas = File.ReadAllLines("Day14Test.txt").Select(l => new Reaction(l))
                .ToDictionary(key => key.Result, val => val);

            var fuelFormula = formulas["FUEL"];
            var totalNeeds = fuelFormula.Needs.ToDictionary(key => key.chem, val => (double) val.amount * neededFuel);

            while (true)
            {
                var singleNeed = totalNeeds.FirstOrDefault(kvp => kvp.Key != "ORE" && kvp.Value > 0);
                if (singleNeed.Key == null)
                    return totalNeeds["ORE"];

                totalNeeds.Remove(singleNeed.Key);
                var needFormula = formulas[singleNeed.Key];
                var factor =  Math.Ceiling(singleNeed.Value / needFormula.ResultAmount);
                var unusedResults = factor * needFormula.ResultAmount - singleNeed.Value;
                foreach (var part in needFormula.Needs)
                {
                    double needAmount;
                    if (totalNeeds.TryGetValue(part.chem, out needAmount))
                        totalNeeds.Remove(part.chem);
                    needAmount += (double)part.amount * factor;
                    totalNeeds[part.chem] = needAmount;
                }

                if (unusedResults > 0)
                {
                    if (totalNeeds.TryGetValue(needFormula.Result, out var curNeed))
                        totalNeeds.Remove(needFormula.Result);
                    curNeed -= unusedResults;
                    totalNeeds[needFormula.Result] = curNeed;
                }
            }
        }

        public object CalcB()
        {
            double test = 1;
            double result = 0;

            while (result < 1000000000000)
            {
                test *= 2;
                result = CalcAWithParam(test);
            }

            double min = test / 2;
            double max = test;

            while (max - min > 1)
            {
                test = (max + min) / 2;
                result = CalcAWithParam(test);
                if (result > 1000000000000)
                    max = test;
                else
                    min = test;
            }

            return min;
        }
        
        
        class Reaction
        {
            public string Result { get; }
            public int ResultAmount { get; }

            public List<(string chem, int amount)> Needs { get; }

            public Reaction(string line)
            {
                var split1 = line.Split(new[] {"=>"}, StringSplitOptions.None);
                (Result, ResultAmount) = ParsePart(split1[1]);
                Needs = split1[0].Split(',').Select(ParsePart).ToList();
            }

            private static (string chem, int amount) ParsePart(string part)
            {
                var split = part.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var chem = split[1];
                var amount = int.Parse(split[0]);
                return (chem, amount);
            }
        }
    }
}