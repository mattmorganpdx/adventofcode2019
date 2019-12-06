using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day05
{
    internal class Program
    {
        private static int Part01(int[] input, int initialInput)
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
                            input[input[pc + 1]] = initialInput;
                            pc += 2;
                            break;
                        case 4:
                            output = input[input[pc + 1]];
                            pc += 2;
                            break;
                        case 5:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            if (input[first] > 0)
                            {
                                pc = input[second];
                            }
                            else
                            {
                                pc += 3;
                            }

                            break;
                        case 6:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            if (input[first] == 0)
                            {
                                pc = input[second];
                            }
                            else
                            {
                                pc += 3;
                            }

                            break;
                        case 7:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            third = input[pc + 3];
                            input[third] = input[first] < input[second] ? 1 : 0;
                            pc += 4;
                            break;
                        case 8:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? pc + 1 : input[pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? pc + 2 : input[pc + 2];
                            third = input[pc + 3];
                            input[third] = input[first] == input[second] ? 1 : 0;
                            pc += 4;
                            break;
                        default:
                            Console.WriteLine($"Found unknown OP Code {opCode.Item1}");
                            return -1;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    return -1;
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


        [Test]
        [TestCase(13787043, 1)] // TestCase Part 1
        [TestCase(3892695, 5)]  // TestCase Part 2
        public void Part01Test(int expected, int initial)
        {
            var input = Files.ReadFileAsArrayOfInt("/home/mmorgan/src/adventofcode2019/Day05/input");
            Assert.AreEqual(expected, Part01(input, initial));
        }
    }
}