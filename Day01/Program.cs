using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day01
{
    internal class Program
    {
        private static int Part01(List<int> input)
        {
            return input.Select(FuelCalculator).Sum();
            
        }

        private static int Part02(List<int> input)
        {
            var totalFuelList = input.Select(mass =>
            {
                var fuelList = new List<int>();
                var tmp = mass;
                while ((tmp = FuelCalculator(tmp)) > 0)
                {
                    fuelList.Add(tmp);
                }

                return fuelList.Sum();
            });
            return totalFuelList.Sum();
        }

        private static int FuelCalculator(int mass)
        {
            // ReSharper disable once PossibleLossOfFraction
            return (int) Math.Floor((decimal) (mass / 3)) - 2;
        }

        [Test]
        public void Part01ExampleTest()
        {
            var input = new List<int>() {12, 14, 1969, 100756};
            Assert.AreEqual(34241, Part01(input));
        }
        
        [Test]
        public void Part01Test()
        {
            var input = Files.ReadLinesAsListOfInt("/home/mmorgan/src/adventofcode2019/Day01/input");
            Assert.AreEqual(3345909, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            // 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346
            var input = new List<int>() {100756};
            Assert.AreEqual(50346, Part02(input));
        }
        
        [Test]
        public void Part02Test()
        {
            var input = Files.ReadLinesAsListOfInt("/home/mmorgan/src/adventofcode2019/Day01/input");
            Assert.AreEqual(5015983, Part02(input));
        }
    }
}