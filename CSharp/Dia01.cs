using System;
using System.Collections.Generic;

namespace CSharp
{
    public class Dia01
    {
        private static (int, int)? Find2(int valor, IReadOnlyList<int> numeros)
        {
            var posActual = 0;
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

        public void Puzzle1(string ruta)
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
                var v1 = resultado.Value.Item1;
                var v2 = resultado.Value.Item2;
                Console.WriteLine("Números: {0}, {1}, {2}", v1, v2, v1 * v2);
            }
            else
            {
                Console.WriteLine("Números no encontrados :c");
            }
        }
    }
}