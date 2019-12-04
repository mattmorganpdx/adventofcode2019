using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day04
{
    internal class Program
    {
        private static int Part01(Tuple<int, int> input)
        {
            /* It is a six-digit number.
             * The value is within the range given in your puzzle input.
             * Two adjacent digits are the same (like 22 in 122345).
             * Going from left to right, the digits never decrease; they only ever increase or stay the same (like 111123 or 135679).
            */
            var (item1, item2) = input;
            return Enumerable
                .Range(item1, item2 - item1 + 1)
                .Where(item => HasAdjacentDigits(item.ToString()))
                .Count(item => DigitsDontDecrease(item.ToString()));
        }

        private static bool HasAdjacentDigits(string password)
        {
            for (var i = 0; i < password.Length - 1; i++)
            {
                if (password.Substring(i, 1) == password.Substring(i + 1, 1))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasAdjacentDigitsOccuringOnlyTwice(string password)
        {
            var results = new List<int>();
            for (var i = 0; i < password.Length - 1; i++)
            {
                var groupSize = GetGroupSizeAtIndex(password, i);
                i += groupSize;
                results.Add(groupSize);
            }
            
            return results.Any(x => x == 1);
        }

        private static int GetGroupSizeAtIndex(string password, int index)
        {
            var groupSize = 0;
            var next = index;
            while (++next < password.Length)
            {
                if (password.Substring(index, 1) == password.Substring(next, 1))
                {
                    ++groupSize;
                } else
                {
                    return groupSize;
                }
            }

            return groupSize;
        }

        private static bool DigitsDontDecrease(string password)
        {
            for (var i = 0; i < password.Length - 1; i++)
            {
                if (Convert.ToInt32(password.Substring(i, 1)) > Convert.ToInt32(password.Substring(i + 1, 1)))
                {
                    return false;
                }
            }

            return true;
        }

        private static int Part02(Tuple<int, int> input)
        {
            var (item1, item2) = input;
            return Enumerable
                .Range(item1, item2 - item1 + 1)
                .Where(item => DigitsDontDecrease(item.ToString()))
                .Count(item => HasAdjacentDigitsOccuringOnlyTwice(item.ToString()));
        }

        [Test]
        [TestCase(true, "111111")]
        [TestCase(false, "223450")]
        [TestCase(true, "123789")]
        public void DigitsDontDecreaseTest(bool expected, string password)
        {
            Assert.AreEqual(expected, DigitsDontDecrease(password));
        }

        [Test]
        [TestCase(true, "111111")]
        [TestCase(true, "223450")]
        [TestCase(false, "123789")]
        public void HasAdjacentDigitsTest(bool expected, string password)
        {
            Assert.AreEqual(expected, HasAdjacentDigits(password));
        }

        [Test]
        [TestCase(true, "112233")]
        [TestCase(false, "123444")]
        [TestCase(true, "111122")]
        [TestCase(false, "124443")]
        public void HasAdjacentDigitsOccuringOnlyTwiceTest(bool expected, string password)
        {
            Assert.AreEqual(expected, HasAdjacentDigitsOccuringOnlyTwice(password));
        }

        [Test]
        public void Part01Test()
        {
            var input = Files.ReadFilesAsTupleOfInt("/home/mmorgan/src/adventofcode2019/Day04/input");
            Assert.AreEqual(1610, Part01(input));
        }


        [Test]
        public void Part02Test()
        {
            // 1404 too high
            var input = Files.ReadFilesAsTupleOfInt("/home/mmorgan/src/adventofcode2019/Day04/input");
            Assert.AreEqual(1610, Part02(input));
        }
    }
}