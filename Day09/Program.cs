using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day09
{
    internal class Program
    {
        private static List<long> Part01(IEnumerable<long> input, List<long> userInput)
        {
            var computer = new Computer(new List<long>(input).ToArray()) {UserInput = new List<long>()};
            if(userInput.Count > 0) computer.UserInput = userInput;
            var outputValues = new List<long>();
            while (!computer.Halted)
            {
                outputValues.Add(computer.RunComputer());
            }


            return outputValues;
        }
        
        [Test]
        public void Part01ExampleTest()
        {
            var userInput = new List<long>();
            var input = new List<long>() {109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99};
            Assert.AreEqual(input, Part01(input, userInput));
            input = new List<long>() {1102, 34915192, 34915192, 7, 4, 7, 99, 0};
            Assert.True(Part01(input, userInput).Last().ToString().Length == 16);
            input = new List<long>() {104, 1125899906842624, 99};
            Assert.AreEqual(1125899906842624, Part01(input, userInput).Last());
        }

        [Test]
        public void Part01Test()
        {
            var userInput = new List<long>() {1};
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day09/input");
            Assert.AreEqual(2350741403, Part01(input, userInput).Last());
        }
        
        [Test]
        public void Part02Test()
        {
            var userInput = new List<long>() {2};
            var input = Files.ReadFileAsArrayOfLong("/home/mmorgan/src/adventofcode2019/Day09/input");
            Assert.AreEqual(53088, Part01(input, userInput).Last());
        }
    }
}