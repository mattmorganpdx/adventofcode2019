using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day06
{
    internal class Program
    {
        private static int Part01(IEnumerable<string> input)
        {
            return CountOrbits("COM", GetOrbitMap(input), 0, new Dictionary<string, int>())
                .Sum(x => x.Value);
        }

        private static Dictionary<string, HashSet<string>> GetOrbitMap(IEnumerable<string> input)
        {
            var orbitMap = new Dictionary<string, HashSet<string>>();
            foreach (var relationship in input)
            {
                var pc = relationship.Split(")");
                var parent = pc[0];
                var child = pc[1];

                if (!orbitMap.ContainsKey(parent)) orbitMap[parent] = new HashSet<string>();

                orbitMap[parent].Add(child);
            }

            return orbitMap;
        }

        private static Dictionary<string, int> CountOrbits(string planet,
            IReadOnlyDictionary<string, HashSet<string>> orbitMap, int counter, Dictionary<string, int> memo)
        {
            if (!orbitMap.ContainsKey(planet))
            {
                memo[planet] = counter + 1;
            }
            else
            {
                memo[planet] = (planet != "COM") ? counter + 1 : 0;
                foreach (var child in orbitMap[planet])
                {
                    memo = CountOrbits(child, orbitMap, memo[planet], memo);
                }
            }

            return memo;
        }

        private static int Part02(List<string> input)
        {
            var orbitMap = GetOrbitMap(input);
            var you = orbitMap.First(x => x.Value.Contains("YOU")).Key;
            var san = orbitMap.First(x => x.Value.Contains("SAN")).Key;
            Console.WriteLine($"You are at {you} and Santa is at {san}");

            var yourPath = GetPath(you, orbitMap);
            var sanPath = GetPath(san, orbitMap);

            var intersection = yourPath.Intersect(sanPath).First();

            return yourPath.IndexOf(intersection) + sanPath.IndexOf(intersection) + 2;
            
        }

        private static List<string> GetPath(string next, Dictionary<string, HashSet<string>> orbitMap)
        {
            var yourPath = new List<string>();
            while (next != "COM")
            {
                next = orbitMap.First(x => x.Value.Contains(next)).Key;
                yourPath.Add(next);
            }

            return yourPath;
        }

        [Test]
        public void Part01ExampleTest()
        {
            var input = new List<string>
            {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L"
            };
            Assert.AreEqual(42, Part01(input));
        }

        [Test]
        public void Part01Test()
        {
            // 25380 too low
            var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input");
            Assert.AreEqual(254447, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            var input = new List<string>
            {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L",
                "K)YOU",
                "I)SAN"
            };
            Assert.AreEqual(4, Part02(input));
        }

        [Test]
        public void Part02Test()
        {
            var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(445, Part02(input));
        }
    }
}