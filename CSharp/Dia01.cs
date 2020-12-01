using System;
using System.Collections.Generic;

namespace CSharp
{
    public static class Dia01
    {
        private static (int, int)? Find2(int valor, IReadOnlyList<int> numeros, int posActual = 0)
        {
            while (posActual < numeros.Count)
            {
                var elemActual = numeros[posActual];
                for (var i = posActual + 1; i < numeros.Count; i++)
                {
                    if (elemActual + numeros[i] == valor)
                    {
                        return (elemActual, numeros[i]);
                    }
                }

                posActual += 1;
            }

            return null;
        }

        public static void Puzzle1(string ruta)
        {
            var lineas = System.IO.File.ReadAllLines(ruta);
            var numeros = new int[lineas.Length];
            for (var i = 0; i < lineas.Length; i++)
            {
                numeros[i] = int.Parse(lineas[i]);
            }

            var resultado = Find2(2020, numeros);
            if (resultado != null)
            {
                var (v1, v2) = resultado.Value;
                Console.WriteLine("Números: {0}, {1}, {2}", v1, v2, v1 * v2);
            }
            else
            {
                Console.WriteLine("Números no encontrados :c");
            }
        }

        private static (int, int, int)? Find3(int valor, IReadOnlyList<int> numeros)
        {
            var posActual = 0;
            while (posActual < numeros.Count)
            {
                var elemActual = numeros[posActual];
                var resto = valor - elemActual;
                for (var i = posActual + 1; i < numeros.Count; i++)
                {
                    var resultadoParcial = Find2(resto, numeros, i);
                    
                    if (resultadoParcial == null) continue;
                    
                    var (v1, v2) = resultadoParcial.Value;
                    return (elemActual, v1, v2);
                }

                posActual += 1;
            }

            return null;
        }
        
        public static void Puzzle2(string ruta)
        {
            var lineas = System.IO.File.ReadAllLines(ruta);
            var numeros = new int[lineas.Length];
            for (var i = 0; i < lineas.Length; i++)
            {
                numeros[i] = int.Parse(lineas[i]);
            }

            var resultado = Find3(2020, numeros);
            if (resultado != null)
            {
                var (v1, v2, v3) = resultado.Value;
                Console.WriteLine("Números: {0}, {1}, {2} -> {3}", v1, v2, v3, v1 * v2 * v3);
            }
            else
            {
                Console.WriteLine("Números no encontrados :c");
            }
        }
    }
}