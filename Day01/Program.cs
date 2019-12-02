using System;
using Microsoft.VisualBasic.CompilerServices;

using NUnit.Framework;
using Utils;

namespace Day01
{
    class Program
    {
        static int Part01()
        {
            return 0;
        }

        static int Part02()
        {
            var data = Files.ReadLines();
            return data[0];
        }
        
        [Test]
        public void Part01Test()
        {
            Assert.AreEqual(0, Part01());
        }

        [Test]
        public void Part02Test()
        {
            Assert.AreEqual(0, Part02());
        }
        
    }
}