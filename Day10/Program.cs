using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;
using NUnit.Framework;
using Utils;

namespace Day10
{
    internal class Program
    {
        private static int Part01(List<Tuple<int, int>> input)
        {
            /*var views = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            foreach (var current in input)
            {
                views[current] = new List<Tuple<int, int>>();
                foreach (var other in input)
                {
                    views[current].Add(Tuple.Create<int, int>(Math.Abs(current.Item1 - other.Item1), Math.Abs(current.Item2 - other.Item2)));
                }
            }

            foreach (var view in views.Keys)
            {
                Console.WriteLine($"Distances for point {view.Item1}, {view.Item2}");
                foreach (var item in views[view])
                {
                    Console.WriteLine($"d: {item.Item1}, {item.Item2}");
                }
            }*/

            /*var pointCounts = new List<Tuple<int, Tuple<int, int>>>();
            foreach (var current in input)
            {
                var westNeighbor = false;
                var northNeighbor = false;
                var eastNeighbor = false;
                var southNeighbor = false;
                var nwNeighbor = false;
                var neNeighbor = false;
                var seNeighbor = false;
                var swNeighbor = false;
                var points = new List<Tuple<int, int>>();

                foreach (var other in input)
                {
                    if (Equals(current, other))
                    {
                        // same so skip.
                        continue;
                    }
                    else if (current.Item1 == other.Item1)
                    {
                        // same column.
                        if (current.Item2 < other.Item2)
                        {
                            northNeighbor = true;
                        }
                        else
                        {
                            southNeighbor = true;
                        }
                    }
                    else if (current.Item2 == other.Item2)
                    {
                        // same row.
                        if (current.Item1 < other.Item1)
                        {
                            westNeighbor = true;
                        }
                        else
                        {
                            eastNeighbor = true;
                        }
                    }
                    else if (Math.Abs(current.Item1 - other.Item1) == Math.Abs(current.Item2 - other.Item2))
                    {
                        // Diagonal cases
                        if (current.Item1 - other.Item1 > 0)
                        {
                            // Westside
                            if (current.Item2 - other.Item2 > 0)
                            {
                                //Northside
                                nwNeighbor = true;
                            }
                            else
                            {
                                swNeighbor = true;
                            }
                        }
                        else
                        {
                            // Eastside
                            if (current.Item2 - other.Item2 > 0)
                            {
                                //Northside
                                neNeighbor = true;
                            }
                            else
                            {
                                seNeighbor = true;
                            }
                        }
                    }
                    else
                    {
                        // All other cases
                        if (points.Count(previous =>
                                CheckDirection(current, other, previous) && !CheckLine(current, other, previous)) ==
                            0) points.Add(other);
                    }

                }
                var dirPoints = 0;
                if (eastNeighbor) dirPoints++;
                if (northNeighbor) dirPoints++;
                if (westNeighbor) dirPoints++;
                if (southNeighbor) dirPoints++;
                if (neNeighbor) dirPoints++;
                if (nwNeighbor) dirPoints++;
                if (swNeighbor) dirPoints++;
                if (seNeighbor) dirPoints++;
                pointCounts.Add(Tuple.Create(dirPoints + points.Count, current));
            }*/


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
                            CheckDirection(current, other, previous) && CheckLine(current, other, previous)) ==
                        0) points.Add(other);
                }

                if (Equals(current, Tuple.Create(3, 4)))
                {
                    foreach (var point in points)
                    {
                        Console.WriteLine($"points in 3, 4: {point.Item1}, {point.Item2}");
                    }
                }

                pointCounts.Add(Tuple.Create<int, Tuple<int, int>>(points.Count, current));
            }


            foreach (var point in pointCounts)
            {
                Console.WriteLine($"Point {point.Item2.Item1}, {point.Item2.Item2} had count of {point.Item1}");
            }

            return pointCounts.OrderByDescending(item => item.Item1).First().Item1;
        }


        private static bool CheckLine(Tuple<int, int> a, Tuple<int, int> b, Tuple<int, int> c)
        {
            return a.Item1 * (b.Item2 - c.Item2) + b.Item1 * (c.Item2 - a.Item2) + c.Item1 * (a.Item2 - b.Item2) ==
                   0;
        }

        private static bool CheckDirection(Tuple<int, int> a, Tuple<int, int> b, Tuple<int, int> c)
        {
            return (a.Item1 - b.Item1 > 0) == (a.Item1 - c.Item1 > 0) &&
                   (a.Item2 - b.Item2 > 0) == (a.Item2 - c.Item2 > 0);
        }

        private static Tuple<int, int> GetDistance(Tuple<int, int> a, Tuple<int, int> b)
        {
            return Tuple.Create(a.Item1 - b.Item1, a.Item2 - b.Item2);
        }

        private static int Part02(List<string> input)
        {
            return 0;
        }

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

            foreach (var (x, y) in input)
            {
                Console.WriteLine($"Asteroid at x: {x} y: {y}");
            }

            Console.WriteLine(input.OrderByDescending(x => x.Item1).First().Item1);
            Console.WriteLine(input.OrderByDescending(x => x.Item2).First().Item2);

            Assert.AreEqual(8, Part01(input));
        }

        [Test]
        public void Part01Test()
        {
            var field = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day10/input");
            var input = GetAsteroids(field);
            // 24 is wrong
            Assert.AreEqual(314, Part01(input));
        }

        [
            Test]
        public void Part02ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [
            Test]
        public void Part02Test()
        {
            // var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(0, 0);
        }
    }
}