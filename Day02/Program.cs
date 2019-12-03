using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day02
{
    internal class Program
    {
        private static int Part01(int[] input)
        {
            var pc = 0;
            int opCode;
            while ((opCode = input[pc]) != 99)
            {
                try
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
                }
                catch (System.IndexOutOfRangeException)
                {
                    return 0;
                }

                pc += 4;
            }
            return input[0];
        }

        private static int Part02(int[] input, int result)
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    int[] freshInput = new int[input.Length];
                    input.CopyTo(freshInput, 0);
                    freshInput[1] = i;
                    freshInput[2] = j;
                    var returnedFromPart01 = Part01(freshInput);
                
                    if (returnedFromPart01 == result) return (100 * i) +j;
                }
            }

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
            // No real example test
            Assert.True(true);
        }
        
        [Test]
        public void Part02Test()
        {
            var result = 19690720;
            var input = Files.ReadFileAsArrayOfInt("/home/mmorgan/src/adventofcode2019/Day02/input");
            Assert.AreEqual(5064, Part02(input, result));
        }
    }
}