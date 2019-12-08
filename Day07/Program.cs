using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Utils;
using Day05;

namespace Day07
{
    internal class Program
    {
        private static int Part01(int[] input)
        {
            const string inputs = "01234";
            var maxScore = 0;
            var keepRunning = true;
            foreach (var current in Permutate(inputs))
            {
                var results = new List<int>();
                foreach (var c in current.ToCharArray())
                {
                    var last = results.Count > 0 ? results.Last() : 0;
                    results.Add(Day05.Program.Part01(input, Convert.ToInt32(c.ToString()), last, false).Item1);
                }

                if (results.Last() > maxScore) maxScore = results.Last();
            }
            return maxScore;
        }

        private static int Part02(int[] input)
        {
            const string inputs = "56789";
            var maxScore = 0;
            foreach (var current in Permutate(inputs))
            {
                var results = new List<int>();
                var shouldContinue = true;
                var firstRun = true;
                var firstInput = true;
                var memory = new Dictionary<char, int[]>();
                var nextInit = new List<int> {0};
                while (shouldContinue)
                {
                    foreach (var c in current.ToCharArray())
                    {
                        var first = firstRun ? Convert.ToInt32(c.ToString()) : nextInit.First();
                        if (!firstRun) nextInit.RemoveAt(0);
                        var localInput = memory.ContainsKey(c) ? memory[c] : input;
                        var (item1, item2, item3) = Day05.Program.Part01(localInput, first, 0, true);
                        results.Add(item1);
                        nextInit.Add(item1);
                        memory[c] = item2;
                        shouldContinue = item3;
                        firstInput = false;
                    }

                    firstRun = false;
                    if (results.Last() > maxScore) maxScore = results.Last();
                }
            }
            
            return maxScore;
        }
 
        private static IEnumerable<string> Permutate(string source)
        {
            if (source.Length == 1) return new List<string> { source };

            var permutations = from c in source
                from p in Permutate(new String(source.Where(x => x != c).ToArray()))
                select c + p;

            return permutations;
        }
        
        [Test]
        public void Part01ExampleTest()
        {
            var input = new List<int>() {3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0}.ToArray();
            Assert.AreEqual(43210, Part01(input));
            input = new List<int>()
                    {3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0}
                .ToArray();
            Assert.AreEqual(54321, Part01(input));
            input = new List<int>()
            {
                3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33, 1002, 33, 7, 33, 1, 33, 31, 31, 1,
                32, 31, 31, 4, 31, 99, 0, 0, 0
            }.ToArray();
            Assert.AreEqual(65210, Part01(input));
        }

        [Test]
        public void Part01Test()
        {
            // 20413 too low
            // 628201814 too high
            var input = Files.ReadFileAsArrayOfInt("/home/mmorgan/src/adventofcode2019/Day07/input");
            Assert.AreEqual(914828, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            var input = new List<int>()
            {
                3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26,
                27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5
            }.ToArray();
            Assert.AreEqual(139629729, Part02(input));

            input = new List<int>()
            {
                3, 52, 1001, 52, -5, 52, 3, 53, 1, 52, 56, 54, 1007, 54, 5, 55, 1005, 55, 26, 1001, 54,
                -5, 54, 1105, 1, 12, 1, 53, 54, 53, 1008, 54, 0, 55, 1001, 55, 1, 55, 2, 53, 55, 53, 4,
                53, 1001, 56, -1, 56, 1005, 56, 6, 99, 0, 0, 0, 0, 10
            }.ToArray();
            Assert.AreEqual(18216, Part02(input));

        }

        [Test]
        public void Part02Test()
        {
            var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(0, 0);
        }
    }
}