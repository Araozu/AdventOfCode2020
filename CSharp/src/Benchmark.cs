using System.Diagnostics;

namespace CSharp
{
    public class Benchmark
    {
        private Stopwatch _sw;

        public void Start()
        {
            _sw = new Stopwatch();
            _sw.Start();
        }

        public void Stop()
        {
            _sw.Stop();
            System.Console.WriteLine("Milisegundos: {0}", _sw.Elapsed.TotalMilliseconds);
        }
    }
}