using System;

namespace CSharp
{
    class Program
    {
        static string getFilePath(int day, int puzzle)
        {
            var dayF = (day.ToString().Length == 1) ? "0" + day : day.ToString();

            return $"/home/araozu/Programacion/AdventOfCode2020/inputs/input_{dayF}_{puzzle}.txt";
        }

        static void Main(string[] _)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            new Dia01().Puzzle1(getFilePath(1, 1));
            sw.Stop();

            Console.WriteLine("Milisegundos: {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}