using System;

namespace CSharp
{
    internal static class Program
    {
        private static string GetFilePath(int day)
        {
            var dayF = day.ToString().Length == 1 ? "0" + day : day.ToString();

            return $"/home/araozu/Programacion/AdventOfCode2020/inputs/input_{dayF}.txt";
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
            */
            
            /*
            bench.Start();
            Dia02.Puzzle01(GetFilePath(2));
            bench.Stop();
            */
            
            bench.Start();
            Dia03.Puzzle(GetFilePath(3));
            bench.Stop();
        }
    }
}