using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day13
{
    internal class Program
    {
        private static int Part01(long[] input)
        {
            var computer = new Computer(input);
            var map = new Dictionary<Tuple<long, long>, long>();
            while (computer.Halted != true)
            {
                var xy = Tuple.Create(computer.RunComputer(), computer.RunComputer());
                map[xy] = computer.RunComputer();
            }
            
            return map.Values.Count(l => l == 2);
        }

        private static int Part02(long[] input)
        {
            input[0] = 2;
            var computer = new Computer(input);
            var map = new Dictionary<Tuple<long, long>, long>();
            while (computer.Halted != true)
            {
                var xy = Tuple.Create(computer.RunComputer(), computer.RunComputer());
                map[xy] = computer.RunComputer();
            }
            
            return 0;

        }

        [Test]
        public void Part01ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part01Test()
        {
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day13/input");
            Assert.AreEqual(236, Part01(input));
        }

        [Test]
        public void Part02ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part02Test()
        {
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day13/input");
            Assert.AreEqual(0, Part02(input));
        }
    }
}