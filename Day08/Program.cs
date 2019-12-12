using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Utils;

namespace Day08
{
    internal class Program
    {
        private static int Part01(List<int> input, int width, int height)
        {
            var length = width * height;
            var layers = new List<List<int>>();
            var layerCount = (input.Count / length);
            for (var i = 0; i < layerCount; i++)
            {
                var layer = new List<int>();
                for (var j = 0; j < length; j++)
                {
                    layer.Add(input[0]);
                    input = input.Skip(1).ToList();
                }
                layers.Add(layer);
            }
            
            var counts = layers.Where(x => x.Contains(0)).Select(layer =>
                {
                    return layer
                        .GroupBy(x => x)
                        .Select(group => new {Metric = @group.Key, Count = @group.Count()})
                        .OrderBy(group => group.Metric);
                })
                .OrderBy(x => x.First().Count).First();


            var enumerable = counts.ToList();
            return enumerable.Where(x => x.Metric == 1).Select(x => x.Count).First() *
                   enumerable.Where(x => x.Metric == 2).Select(x => x.Count).First();
        }


        private static int Part02(List<string> input)
        {
            return 0;
        }

        [Test]
        public void Part01ExampleTest()
        {
            var input = "123456789012".ToCharArray().Select(x => Convert.ToInt32(x.ToString())).ToList();
            Assert.AreEqual(1, Part01(input, 3, 2));
        }

        [Test]
        public void Part01Test()
        {
            var input = Utils.Files.ReadFileAsLineOfInt("/home/mmorgan/src/adventofcode2019/Day08/input");
            Assert.AreEqual(2356, Part01(input, 25, 6));
        }

        [Test]
        public void Part02ExampleTest()
        {
            Assert.AreEqual(0, 0);
        }

        [Test]
        public void Part02Test()
        {
            // var input = System.IO.File.ReadLines("/home/mmorgan/src/adventofcode2019/Day06/input").ToList();
            Assert.AreEqual(0, 0);
        }
    }
}