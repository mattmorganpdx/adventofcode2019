using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using Utils;

namespace Day11
{
    internal class Program
    {
        private static int Part01(long[] input)
        {
            var computer = new Computer(input);
            var turn = Turn();
            var current = (0, 0);

            var map = new Dictionary<(int, int), (long, Func<long, bool>, int)> {[current] = (0, GetLock(), 0)};

            while (!computer.Halted)
            {
                var (paint, toggle, counter) = map.GetValueOrDefault(current, (0, GetLock(), 0));
                computer.UserInput.Add(paint);
                paint = computer.RunComputer();
                if (toggle(paint)) ++counter;
                map[current] = (paint, toggle, counter);
                
                
                var (x, y) = turn(computer.RunComputer());
                current = (current.Item1 + x, current.Item2 + y);
                
            }
            
            var minX = map.Keys.Min(x => x.Item1);
            var minY = map.Keys.Min(y => y.Item2);
            var maxX = map.Keys.Max(x => x.Item1);
            var maxY = map.Keys.Max(y => y.Item2);
            Console.WriteLine($"min X is {minX} Min Y is {minY}");
            Console.WriteLine($"max X is {maxX} Max Y is {maxY}");

            var formattedOutput = new List<List<char>>();

            for(var y = Math.Abs(minY); y >= maxY; y--)
            {
                formattedOutput.Add(new List<char>());
                for (var x = minX; x < maxX; x++)
                {
                    if (map.TryGetValue((x, y + minY), out var outputValue))
                    {
                        formattedOutput[^1].Add(outputValue.Item1 == 1 ? '#' : ' ');    
                    } else
                    {
                        formattedOutput[^1].Add(' ');
                    }
                    
                } 
            }

            // BFEAGHAF
            foreach (var line in formattedOutput)
            {
                Console.WriteLine(new string(line.ToArray()));
            }

            return map.Values.Count(item => item.Item3 > 0);
        }

        private static int Part02(long[] input)
        {
            var computer = new Computer(input);
            var turn = Turn();
            var current = (0, 0);

            var map = new Dictionary<(int, int), (long, Func<long, bool>, int)> {[current] = (1, GetLock(), 0)};

            while (!computer.Halted)
            {
                var (paint, toggle, counter) = map.GetValueOrDefault(current, (0, GetLock(), 0));
                computer.UserInput.Add(paint);
                paint = computer.RunComputer();
                if (toggle(paint)) ++counter;
                map[current] = (paint, toggle, counter);
                
                
                var (x, y) = turn(computer.RunComputer());
                current = (current.Item1 + x, current.Item2 + y);
                
            }

            var minX = map.Keys.Min(x => x.Item1);
            var minY = map.Keys.Min(y => y.Item2);
            var maxX = map.Keys.Max(x => x.Item1);
            var maxY = map.Keys.Max(y => y.Item2);
            Console.WriteLine($"min X is {minX} Min Y is {minY}");
            Console.WriteLine($"max X is {maxX} Max Y is {maxY}");

            var formattedOutput = new List<List<char>>();

            for(var y = Math.Abs(minY); y >= maxY; y--)
            {
                formattedOutput.Add(new List<char>());
                for (var x = minX; x < maxX; x++)
                {
                    if (map.TryGetValue((x, y + minY), out var outputValue))
                    {
                        formattedOutput[^1].Add(outputValue.Item1 == 1 ? '#' : ' ');    
                    } else
                    {
                        formattedOutput[^1].Add(' ');
                    }
                    
                } 
            }

            // BFEAGHAF
            foreach (var line in formattedOutput)
            {
                Console.WriteLine(new string(line.ToArray()));
            }
            

            return map.Values.Count(item => item.Item3 > 0);
        }

        private static Func<long, (int, int)> Turn()
        {
            var state = Complex.ImaginaryOne;

            var directions = new Dictionary<Complex, (int, int)>
            {
                [Complex.ImaginaryOne] = (0, 1),
                [Complex.One] = (1, 0),
                [-Complex.ImaginaryOne] = (0, -1),
                [-Complex.One] = (-1, 0)
            };
            return delegate(long wise)
            {
                state = wise == 0 ? state * Complex.ImaginaryOne : state * -Complex.ImaginaryOne;
                return directions[state];
            };
        }

        private static Func<long, bool> GetLock()
        {
            long _lock = 0;
            return delegate(long input)
            {
                if (_lock == input) return false;
                _lock = input;
                return true;
            };
        }

        [Test]
        public void GetLockTest()
        {
            var myLock = GetLock();
            Assert.AreEqual(false, myLock(0));
            Assert.AreEqual(true, myLock(1));
            Assert.AreEqual(false, myLock(1));
            Assert.AreEqual(true, myLock(0));
        }

        [Test]
        public void ImagTest()
        {
            var turn = Turn();
            Assert.AreEqual((-1, 0), turn(0));
            Assert.AreEqual((0, -1), turn(0));
        }

        [Test]
        public void Part01ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part01Test()
        {
            // 1008 too low
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day11/input");
            Assert.AreEqual(1885, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part02Test()
        {
            // BFEAGHAF
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day11/input");
            Assert.AreEqual(107, Part02(input));
        }
    }
}