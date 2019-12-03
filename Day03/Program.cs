using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Utils;

namespace Day03
{
    internal class Program
    {
        private static int Part01(List<string[]> input)
        {
            var wire1 = ParseWireRoute(input[0]);
            var wire2 = ParseWireRoute(input[1]);
            var routeWire1 = new List<Tuple<int, int>> {new Tuple<int, int>(0, 0)};
            foreach (var turn in wire1)
            {
                DrawWire(routeWire1, turn);
            }
            var routeWire2 = new List<Tuple<int, int>> {new Tuple<int, int>(0, 0)};
            foreach (var turn in wire2)
            {
                DrawWire(routeWire2, turn);
            }

            var intersection = routeWire1.Intersect(routeWire2);
            return intersection.Select(item => Math.Abs(item.Item1) + Math.Abs(item.Item2)).Where(x => x != 0).OrderBy(x => x).First();
            
        }

        private static int Part02(List<string[]> input)
        {
            var wire1 = ParseWireRoute(input[0]);
            var wire2 = ParseWireRoute(input[1]);
            var routeWire1 = new List<Tuple<int, int>> {new Tuple<int, int>(0, 0)};
            foreach (var turn in wire1)
            {
                DrawWire(routeWire1, turn);
            }
            var routeWire2 = new List<Tuple<int, int>> {new Tuple<int, int>(0, 0)};
            foreach (var turn in wire2)
            {
                DrawWire(routeWire2, turn);
            }

            var intersection = routeWire1.Intersect(routeWire2);
            return intersection.Select(item => Math.Abs(item.Item1) + Math.Abs(item.Item2)).Where(x => x != 0).OrderBy(x => x).First();
        }

        private static IEnumerable<Tuple<string, int>> ParseWireRoute(IEnumerable<string> route)
        {
            return route.Select(turn => new Tuple<string, int>(turn.Substring(0, 1), Convert.ToInt32(turn.Substring(1)))).ToList();
        }

        private static int DrawWire(ICollection<Tuple<int, int>> path, Tuple<string, int> turn)
        {
            var (item1, item2) = path.Last();
            switch (turn.Item1)
            {
                case "U":
                    for (var i = 1; i <= turn.Item2; i++)
                    {
                        path.Add(new Tuple<int, int>(item1, item2 + i));   
                    }
                    break;
                case "D":
                    for (var i = 1; i <= turn.Item2; i++)
                    {
                        path.Add(new Tuple<int, int>(item1, item2 - i));   
                    }
                    break;
                case "R":
                    for (var i = 1; i <= turn.Item2; i++)
                    {
                        path.Add(new Tuple<int, int>(item1 + i, item2));    
                    }
                    break;
                case "L":
                    for (var i = 1; i <= turn.Item2; i++)
                    {
                        path.Add(new Tuple<int, int>(item1 - i, item2));    
                    }
                    break;
                default:
                    return 0;
            }

            return turn.Item2;
        }

        [Test]
        public void Part01ExampleTest()
        {
            var input1 = new List<string[]>();
            input1.Add("R8,U5,L5,D3".Split(","));
            input1.Add("U7,R6,D4,L4".Split(","));
            Assert.AreEqual(6, Part01(input1));
            
            var input2 = new List<string[]>();
            input2.Add("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(","));
            input2.Add("U62,R66,U55,R34,D71,R55,D58,R83".Split(","));
            Assert.AreEqual(159, Part01(input2));
            
            var input3 = new List<string[]>();
            input3.Add("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(","));
            input3.Add("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(","));
            Assert.AreEqual(135, Part01(input3));
        }
        
        [Test]
        public void Part01Test()
        {
            // 299 too high
            // fixed with off-by-one issue in the Tuple generator
            var input = Files.ReadFileAsListOfStringList("/home/mmorgan/src/adventofcode2019/Day03/input");
            Assert.AreEqual(293, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            var input1 = new List<string[]>();
            input1.Add("R8,U5,L5,D3".Split(","));
            input1.Add("U7,R6,D4,L4".Split(","));
            Assert.AreEqual(6, Part01(input1));
            
            var input2 = new List<string[]>();
            input2.Add("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(","));
            input2.Add("U62,R66,U55,R34,D71,R55,D58,R83".Split(","));
            Assert.AreEqual(159, Part01(input2));
            
            var input3 = new List<string[]>();
            input3.Add("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(","));
            input3.Add("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(","));
            Assert.AreEqual(135, Part01(input3));
        }
        
        [Test]
        public void Part02Test()
        {
            var input = Files.ReadFileAsListOfStringList("/home/mmorgan/src/adventofcode2019/Day03/input");
            Assert.AreEqual(293, Part01(input));
        }
    }
}