using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day01
{
    internal class Program
    {
        private static int Part01(int[] input)
        {
            var pc = 0;
            int opCode;
            while ((opCode = input[pc]) != 99)
            {
                switch (opCode)
                {
                    case 1:
                        input[input[pc + 3]] = input[input[pc + 1]] + input[input[pc + 2]];
                        break;
                    case 2:
                        input[input[pc + 3]] = input[input[pc + 1]] * input[input[pc + 2]];
                        break;
                    default:
                        Console.WriteLine($"Found unknown OP Code {opCode}");
                        return 0;
                }

                pc += 4;
            }
            Console.WriteLine(input.Length);
            return input[0];
        }

        private static int Part02(List<int> input)
        {
            return 0;
        }
        

        [Test]
        public void Part01ExampleTest()
        {
            int[] input = {1,9,10,3,2,3,11,0,99,30,40,50};
            //{3500,9,10,70,2,3,11,0,99,30,40,50}
            Assert.AreEqual(3500, Part01(input));
        }
        
        [Test]
        public void Part01Test()
        {
            var input = Files.ReadFileAsArrayOfInt("/home/mmorgan/src/adventofcode2019/Day02/input");
            // This replacement is part of the challenge that would break the Example Test
            input[1] = 12;
            input[2] = 2;
            Assert.AreEqual(5098658, Part01(input));
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