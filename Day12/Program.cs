using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using Utils;

namespace Day12
{
    internal class Program
    {
        private static float Part01(List<(Vector3, Vector3)> input, int maxCount)
        {
            var counter = 0;

            while (++counter <= maxCount)
            {
                input = GetNextInput(input, counter);
            }

            return input.Select(x => SumVector(x.Item1) * SumVector(x.Item2)).Sum();
        }

        private static List<(Vector3, Vector3)> GetNextInput(IReadOnlyCollection<(Vector3, Vector3)> input, int counter)
        {
            var nextInput = new List<(Vector3, Vector3)>();
            foreach (var i in input)
            {
                var newI = (CopyPlanet(i.Item1), CopyPlanet(i.Item2));
                foreach (var j in input)
                {
                    if (j.Equals(i)) continue;

                    newI.Item2 += ComparePositions(i.Item1, j.Item1);
                    //Console.WriteLine(newI.Item2.X);
                }

                newI.Item1 += newI.Item2;
                nextInput.Add(newI);

                /*if (counter % 10 != 0) continue;*/
                //PrintPlanets(nextInput);
            }
            //PrintPlanets(input);

            return nextInput;
        }

        private static void PrintPlanets(IEnumerable<(Vector3, Vector3)> input)
        {
            foreach (var (p, v) in input)
            {
                Console.WriteLine($"Position: {p.X}, {p.Y}, {p.Z} Velocity: {v.X}, {v.Y}, {v.Z}");
            }
        }

        private static long Part02(List<(Vector3, Vector3)> input)
        {
            var history = new Dictionary<float, List<List<(Vector3,Vector3)>>>();

            var counter = 0L;
            do
            {
                if (history.ContainsKey(SumVectors(input)))
                {
                    foreach (var possibleMatch in history[SumVectors(input)])
                    {
                        if (CompareSystems(input, possibleMatch)) return counter;
                    }
                }
                else
                {
                    history.Add(SumVectors(input), new List<List<(Vector3, Vector3)>>());
                }
                var inputArr = new (Vector3,Vector3)[4];
                input.CopyTo(inputArr);
                history[SumVectors(input)].Add(inputArr.ToList());
                
                
                input = GetNextInput(input, 0);
                counter++;
            } while (true);
            
        }

        private static bool CompareSystems(IEnumerable<(Vector3, Vector3)> a, IEnumerable<(Vector3, Vector3)> b)
        {
            return a.Zip(b, (first, second) => first == second).All(x => x);
        }
        
        private static readonly Func<List<(Vector3, Vector3)>, float> SumVectors = (v) =>
            v.Select(x => SumVector(x.Item1) * SumVector(x.Item2)).Sum();

        private static readonly Func<Vector3, float> SumVector = (v) => Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z);

        private static readonly Func<Vector3, Vector3> CopyPlanet = (oldVector) =>
            new Vector3(oldVector.X, oldVector.Y, oldVector.Z);

        private static readonly Func<Vector3, Vector3, Vector3> ComparePositions = (a, b) =>
            new Vector3(ComparePosition(a.X, b.X), ComparePosition(a.Y, b.Y), ComparePosition(a.Z, b.Z));

        private static readonly Func<float, float, float> ComparePosition = (a, b) =>
        {
            if (a > b) return -1;
            return a < b ? 1 : 0;
        };


        [Test]
        public void Part01ExampleTest01()
        {
            /*<x=-1, y=0, z=2>
            <x=2, y=-10, z=-7>
            <x=4, y=-8, z=8>
            <x=3, y=5, z=-1>*/
            var a = new Vector3(-1, 0, 2);
            var b = new Vector3(2, -10, -7);
            var c = new Vector3(4, -8, 8);
            var d = new Vector3(3, 5, -1);

            var planets = new List<(Vector3, Vector3)>()
            {
                (a, new Vector3(0, 0, 0)),
                (b, new Vector3(0, 0, 0)),
                (c, new Vector3(0, 0, 0)),
                (d, new Vector3(0, 0, 0)),
            };
            Assert.AreEqual(179, (int) Part01(planets, 10));
        }

        [Test]
        public void Part01ExampleTest02()
        {
            /*  <x=-8, y=-10, z=0>
                <x=5, y=5, z=10>
                <x=2, y=-7, z=3>
                <x=9, y=-8, z=-3>
            */
            var a = new Vector3(-8, -10, 0);
            var b = new Vector3(5, 5, 10);
            var c = new Vector3(2, -7, 3);
            var d = new Vector3(9, -8, -3);

            var planets = new List<(Vector3, Vector3)>()
            {
                (a, new Vector3(0, 0, 0)),
                (b, new Vector3(0, 0, 0)),
                (c, new Vector3(0, 0, 0)),
                (d, new Vector3(0, 0, 0)),
            };
            Assert.AreEqual(1940, (int) Part01(planets, 100));
        }

        [Test]
        public void Part01Test()
        {
            /*
                <x=-16, y=15, z=-9>
                <x=-14, y=5, z=4>
                <x=2, y=0, z=6>
                <x=-3, y=18, z=9>
             */
            var a = new Vector3(-16, 15, -9);
            var b = new Vector3(-14, 5, 4);
            var c = new Vector3(2, 0, 6);
            var d = new Vector3(-3, 18, 9);

            var planets = new List<(Vector3, Vector3)>()
            {
                (a, new Vector3(0, 0, 0)),
                (b, new Vector3(0, 0, 0)),
                (c, new Vector3(0, 0, 0)),
                (d, new Vector3(0, 0, 0)),
            };
            //1497
            Assert.AreEqual(10664, (int) Part01(planets, 1000));
        }

        [Test]
        public void Part02ExampleTest01()
        {
            var a = new Vector3(-1, 0, 2);
            var b = new Vector3(2, -10, -7);
            var c = new Vector3(4, -8, 8);
            var d = new Vector3(3, 5, -1);

            var planets = new List<(Vector3, Vector3)>()
            {
                (a, new Vector3(0, 0, 0)),
                (b, new Vector3(0, 0, 0)),
                (c, new Vector3(0, 0, 0)),
                (d, new Vector3(0, 0, 0)),
            };
            Assert.AreEqual(2772, Part02(planets));
        }

        [Test]
        public void Part02ExampleTest02()
        {
            var a = new Vector3(-8, -10, 0);
            var b = new Vector3(5, 5, 10);
            var c = new Vector3(2, -7, 3);
            var d = new Vector3(9, -8, -3);

            var planets = new List<(Vector3, Vector3)>()
            {
                (a, new Vector3(0, 0, 0)),
                (b, new Vector3(0, 0, 0)),
                (c, new Vector3(0, 0, 0)),
                (d, new Vector3(0, 0, 0)),
            };
            Assert.AreEqual(4686774924, Part02(planets));
        }
        
        [Test]
        public void Part02Test()
        {
            // var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(0, 0);
        }
    }
}