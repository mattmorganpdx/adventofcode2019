using System;
using System.Collections.Generic;
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
        static void Main(string[] args) { }
    }
}