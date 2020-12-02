using System;
using System.Text.RegularExpressions;

namespace CSharp
{
    internal class Password
    {
        private readonly int _minRepeticiones;
        private readonly int _maxRepeticiones;
        private readonly char _letra;
        private readonly string _contrasena;

        private Password(int minRepeticiones, int maxRepeticiones, char letra, string contrasena)
        {
            _minRepeticiones = minRepeticiones;
            _maxRepeticiones = maxRepeticiones;
            _letra = letra;
            _contrasena = contrasena;
        }

        private int CharCount()
        {
            var count = 0;
            foreach (var c in _contrasena.ToCharArray())
            {
                if (c == _letra) count++;
            }

            return count;
        }
        
        public bool IsValidMetric1()
        {
            var charCount = CharCount();
            return charCount >= _minRepeticiones && charCount <= _maxRepeticiones;
        }

        public bool IsValidMetric2()
        {
            var char1 = _contrasena[_minRepeticiones - 1];
            var char2 = _contrasena[_maxRepeticiones - 1];

            return char1 != char2 && (char1 == _letra || char2 == _letra);
        }

        public static Password FromString(string data)
        {
            var rx = new Regex(@"(\d+)-(\d+) (\w): (.+)");
            var matches = rx.Matches(data);
            foreach (Match match in matches)
            {
                var group = match.Groups;
                var min = int.Parse(group[1].Value);
                var max = int.Parse(group[2].Value);
                var letra = group[3].Value[0];
                var contrasena = group[4].Value;

                return new Password(min, max, letra, contrasena);
            }

            throw new ArgumentException("El string provisto no cumple con el formato.");
        }
    }

    public static class Dia02
    {
        public static void Puzzle01(string rutaArchivo)
        {
            var lineas = System.IO.File.ReadLines(rutaArchivo);
            var validCount1 = 0;
            var validCount2 = 0;
            foreach (var linea in lineas)
            {
                var password = Password.FromString(linea);
                if (password.IsValidMetric1()) validCount1++;
                if (password.IsValidMetric2()) validCount2++;
            }
            
            Console.WriteLine("Contraseñas validas por 1ra heuristica: {0}", validCount1);
            Console.WriteLine("Contraseñas validas por 2da heuristica: {0}", validCount2);
        }
    }
}