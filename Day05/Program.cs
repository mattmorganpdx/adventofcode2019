using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day05
{
    internal class Program
    {
        private static int Part01(int[] input)
        {
            var output = 0;
            var pc = 0;
            Tuple<int, List<int>> opCode;
            while ((opCode = ProcessOpCode(input[pc])).Item1 != 99)
            {
                try
                {
                    var first = 0;
                    var second = 0;
                    var third = 0;
                    switch (opCode.Item1)
                    {
                        case 1:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            third = input[pc + 3];
                            input[third] = input[first] + input[second];
                            pc += 4;
                            break;
                        case 2:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            third = input[pc + 3];
                            input[third] = input[first] * input[second];
                            pc += 4;
                            break;
                        case 3:
                            input[input[pc + 1]] = 1;
                            pc += 2;
                            break;
                        case 4:
                            Console.WriteLine($"Output: {input[input[pc + 1]]}");
                            output = input[input[pc + 1]];
                            pc += 2;
                            break;
                        default:
                            Console.WriteLine($"Found unknown OP Code {opCode.Item1}");
                            return -1;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine($"some index error {opCode.Item1} {opCode.Item2.Count}");
                    return 0;
                }

                
            }
            return output;
        }

        private static Tuple<int, List<int>> ProcessOpCode(int opCode)
        {
            var digits = opCode.ToString("D5");
            var op = Convert.ToInt32(digits.Substring(digits.Length - 2, 2));
            var modes = digits.Substring(0, digits.Length - 2)
                .Select(x => int.Parse(x.ToString()))
                .Reverse()
                .ToList();
            return Tuple.Create(op, modes);
        }

        [Test]
        public void ProcessOpCodeTest()
        {
            var expected = Tuple.Create(2, new List<int>() {0, 1, 0});
            Assert.AreEqual(expected, ProcessOpCode(1002));
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
        public void Part01Test()
        {
            var input = Files.ReadFileAsArrayOfInt("/home/mmorgan/src/adventofcode2019/Day05/input");
            Assert.AreEqual(13787043, Part01(input));
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