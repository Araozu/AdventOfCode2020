using System;

namespace CSharp
{
    public static class Dia03
    {
        private static int GetTreeAmount(string[] slopeData, int right, int down)
        {
            var currentPos = 0;
            var treeAmount = 0;
            for (var i = 0; i < slopeData.Length; i += down)
            {
                var line = slopeData[i];
                var c = line[currentPos];
                if (c == '#')
                {
                    treeAmount += 1;
                }

                currentPos = (currentPos + right) % line.Length;
            }

            return treeAmount;
        }

        public static void Puzzle(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            var treeAmount1 = GetTreeAmount(lines, 1, 1);
            var treeAmount2 = GetTreeAmount(lines, 3, 1);
            var treeAmount3 = GetTreeAmount(lines, 5, 1);
            var treeAmount4 = GetTreeAmount(lines, 7, 1);
            var treeAmount5 = GetTreeAmount(lines, 1, 2);

            Console.WriteLine("First tree amount: {0}", treeAmount2);
            var totalTreeAmount = treeAmount1 * treeAmount2 * treeAmount3 * treeAmount4 * treeAmount5;
            Console.WriteLine("Total tree amount: {0}", totalTreeAmount);
        }
    }
}