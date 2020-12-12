using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssa_dat
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"../../ssa dat.txt");
            var table = lines.Select(line =>
            {
                line = line.Replace(" ", "");
                var components = line.Split('\t');
                var age = Convert.ToInt32(components[0]);
                var maleDeathProbability = Convert.ToDouble(components[1]);
                var femaleDeathProbability = Convert.ToDouble(components[4]);
                var deathProbability = (maleDeathProbability + femaleDeathProbability) / 2;

                return new AgeDeathRate()
                {
                    Age = age,
                    DeathProbability = deathProbability
                };
            }).ToList();

            var _0_19_deathRate = GetAgeRangeDeathRate(table, 0, 19);
            var _20_49_deathRate = GetAgeRangeDeathRate(table, 20, 49);
            var _50_69_deathRate = GetAgeRangeDeathRate(table, 50, 69);
            var _70_plus_deathRate = GetAgeRangeDeathRate(table, 70, Int32.MaxValue);
        }

        static double GetAgeRangeDeathRate(List<AgeDeathRate> table, int startAge, int endAge)
        {
            var inRange = table.Where(adr => adr.Age >= startAge && adr.Age <= endAge).ToList();
            return inRange.Average(adr => adr.DeathProbability * 100);
        }
    }

    class AgeDeathRate
    {
        public int Age;
        public double DeathProbability;
    }
}
