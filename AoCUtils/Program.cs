using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Utils
{
    public static class Files
    {
        public static List<int> ReadLinesAsListOfInt(string aFileName)
        {
            var lines = new List<string>(System.IO.File.ReadAllLines(aFileName));
            return new List<int>(lines.Select(line => Convert.ToInt32(line)));
        }

        public static int[] ReadFileAsArrayOfInt(string aFileName)
        {
            var line = System.IO.File.ReadAllText(aFileName);
            return line.Split(",").Select(item => Convert.ToInt32(item)).ToArray();
        }

        public static List<string[]> ReadFileAsListOfStringList(string aFileName)
        {
            var lines = System.IO.File.ReadLines(aFileName);
            var input = new List<string[]>();
            foreach (var line in lines)
            {
                input.Add(line.Split(","));
            }

            return input;
        }
        static void Main(string[] args) { }
    }
}