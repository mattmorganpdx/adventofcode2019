using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day10
{
    internal class Program
    {
        private static int Part01(IReadOnlyCollection<Tuple<int, int>> input)
        {
            var pointCounts = new List<Tuple<int, Tuple<int, int>>>();
            foreach (var current in input)
            {
                var points = new List<Tuple<int, int>>();
                foreach (var other in input)
                {
                    if (Equals(current, other))
                    {
                        // same so skip.
                        continue;
                    }

                    if (points.Count(previous =>
                            SameSide(current, other, previous) && IsLine(current, other, previous)) ==
                        0) points.Add(other);
                }

                pointCounts.Add(Tuple.Create<int, Tuple<int, int>>(points.Count, current));
            }

            var bestPoint = pointCounts.OrderByDescending(item => item.Item1).First();
            Console.WriteLine($"Best Point is at x:{bestPoint.Item2.Item1} y:{bestPoint.Item2.Item2}");
            return bestPoint.Item1;
        }

        private static readonly Func<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>, bool> IsLine = (a, b, c) =>
            a.Item1 * (b.Item2 - c.Item2) + b.Item1 * (c.Item2 - a.Item2) + c.Item1 * (a.Item2 - b.Item2) ==
            0;

        private static readonly Func<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>, bool> SameSide = (a, b, c) =>
            (a.Item1 - b.Item1 > 0) == (a.Item1 - c.Item1 > 0) &&
            (a.Item2 - b.Item2 > 0) == (a.Item2 - c.Item2 > 0);


        private static List<Tuple<int, int>> GetAsteroids(IEnumerable<string> field)
        {
            return (from row in field.Select((value, index) => new {Value = value, Index = index})
                from column in row.Value.ToCharArray().Select((value, index) => new {Value = value, Index = index})
                where column.Value == '#'
                select Tuple.Create(column.Index, row.Index)).ToList();
        }

        [Test]
        public void Part01ExampleTest()
        {
            var field = new List<string>()
            {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };

            var input = GetAsteroids(field);

            Assert.AreEqual(8, Part01(input));
        }

        [Test]
        public void Part01Test()
        {
            var field = File.ReadLines("/home/mmorgan/src/adventofcode2019/Day10/input");
            var input = GetAsteroids(field);
            // 24 is wrong
            Assert.AreEqual(314, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part02Test()
        {
            // Best Point from Part01 {x: 27, y: 19}
            // var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(0, 0);
        }
    }
}