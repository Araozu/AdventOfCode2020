using System;
using System.Collections.Generic;

namespace CSharp
{
    internal static class Program
    {
        private static string GetFilePath(int day)
        {
            var dayF = day.ToString().Length == 1 ? "0" + day : day.ToString();

            return $"/home/araozu/Programacion/AdventOfCode2020/inputs/input_{dayF}.txt";
        }

        private static string GetFilePath(int day, object _)
        {
            var dayF = day.ToString().Length == 1 ? "0" + day : day.ToString();

            return $"/home/araozu/Programacion/AdventOfCode2020/inputs/input_{dayF}_test.txt";
        }

        private static string[] GetData(int day)
        {
            return System.IO.File.ReadAllLines(GetFilePath(day));
        }

        private static string[] GetData(int day, object _)
        {
            return System.IO.File.ReadAllLines(GetFilePath(day, _));
        }

        private static void Main(string[] _)
        {
            var bench = new Benchmark();
            /*
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Dia01.Puzzle1(GetFilePath(1));
            sw.Stop();

            Console.WriteLine("Milisegundos: {0}", sw.Elapsed.TotalMilliseconds);

            var sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();
            Dia01.Puzzle2(GetFilePath(1));
            sw2.Stop();
            Console.WriteLine("Milisegundos: {0}", sw2.Elapsed.TotalMilliseconds);
            
            bench.Start();
            Dia02.Puzzle01(GetFilePath(2));
            bench.Stop();
            
            bench.Start();
            Dia03.Puzzle(GetFilePath(3));
            bench.Stop();
            
            bench.Start();
            var validCount = Dia04.Puzzle(lines);
            bench.Stop();
            // The solution to the first part was 179
            Console.WriteLine("Valid passports with metric 1: {0}", validCount);
            */

            var lines = GetData(11);
            bench.Start();
            Dia11.Puzzle(lines);
            bench.Stop();
        }
    }
}