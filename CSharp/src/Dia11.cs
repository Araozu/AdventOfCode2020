using System;

namespace CSharp
{
    internal enum S
    {
        Empty,
        Full,
        Floor
    }

    internal class TableTester
    {
        private readonly S[][] _table;
        private readonly S _defaultReturn;
        private readonly int _i;
        private readonly int _j;
        private readonly int _iLimit;
        private readonly int _jLimit;

        public TableTester(S[][] table, S defaultReturn, int i, int j, int iLimit, int jLimit)
        {
            _table = table;
            _defaultReturn = defaultReturn;
            _i = i;
            _j = j;
            this._iLimit = iLimit;
            this._jLimit = jLimit;
        }

        private S GetFirstSeat(int iChange, int jChange, int iLimit, int jLimit)
        {
            var nextI = _i + iChange;
            var nextJ = _j + jChange;

            while (nextI != iLimit && nextJ != jLimit)
            {
                var nextValue = _table[nextI][nextJ];

                if (nextValue != S.Floor) return nextValue;

                nextI += iChange;
                nextJ += jChange;
            }

            return _defaultReturn;
        }

        public S GetTopLeft()
        {
            return GetFirstSeat(-1, -1, -1, -1);
        }

        public S GetTop()
        {
            return GetFirstSeat(-1, 0, -1, -1);
        }

        public S GetTopRight()
        {
            return GetFirstSeat(-1, 1, -1, _jLimit);
        }

        public S GetLeft()
        {
            return GetFirstSeat(0, -1, -1, -1);
        }

        public S GetRight()
        {
            return GetFirstSeat(0, 1, -1, _jLimit);
        }

        public S GetBottomLeft()
        {
            return GetFirstSeat(1, -1, _iLimit, -1);
        }

        public S GetBottom()
        {
            return GetFirstSeat(1, 0, _iLimit, -1);
        }

        public S GetBottomRight()
        {
            return GetFirstSeat(1, 1, _iLimit, _jLimit);
        }
    }

    public static class Dia11
    {
        private static void PrintTable(S[][] table)
        {
            foreach (var variable in table)
            {
                foreach (var se in variable)
                {
                    switch (se)
                    {
                        case S.Empty:
                            Console.Write('L');
                            break;
                        case S.Floor:
                            Console.Write('.');
                            break;
                        case S.Full:
                            Console.Write('#');
                            break;
                    }
                }

                Console.WriteLine();
            }
        }

        private static S[][] CloneTable(S[][] table)
        {
            var newTable = new S[table.Length][];
            var width = table[0].Length;
            for (var i = 0; i < table.Length; i++)
            {
                newTable[i] = new S[width];
                for (var j = 0; j < width; j++)
                {
                    newTable[i][j] = table[i][j];
                }
            }

            return newTable;
        }

        private static int CountOccupiedSeats(S[][] table)
        {
            var count = 0;
            foreach (var se in table)
            {
                foreach (var se1 in se)
                {
                    if (se1 == S.Full)
                    {
                        count++;
                    }
                }
            }

            return count;
        }


        private static S TestChange2(S[][] table, S value, int i, int j)
        {
            var topILimit = table.Length;
            var topJLimit = table[0].Length;

            if (value == S.Empty)
            {
                var tester = new TableTester(table, S.Empty, i, j, topILimit, topJLimit);

                var topLeft = tester.GetTopLeft() == S.Empty;
                var top = tester.GetTop() == S.Empty;
                var topRight = tester.GetTopRight() == S.Empty;
                var left = tester.GetLeft() == S.Empty;
                var right = tester.GetRight() == S.Empty;
                var bottomLeft = tester.GetBottomLeft() == S.Empty;
                var bottom = tester.GetBottom() == S.Empty;
                var bottomRight = tester.GetBottomRight() == S.Empty;

                return topLeft && top && topRight && left && right && bottomLeft && bottom && bottomRight
                    ? S.Full
                    : value;
            }

            if (value == S.Full)
            {
                var tester = new TableTester(table, S.Floor, i, j, topILimit, topJLimit);

                var topLeft = tester.GetTopLeft() == S.Full ? 1 : 0;
                var top = tester.GetTop() == S.Full ? 1 : 0;
                var topRight = tester.GetTopRight() == S.Full ? 1 : 0;
                var left = tester.GetLeft() == S.Full ? 1 : 0;
                var right = tester.GetRight() == S.Full ? 1 : 0;
                var bottomLeft = tester.GetBottomLeft() == S.Full ? 1 : 0;
                var bottom = tester.GetBottom() == S.Full ? 1 : 0;
                var bottomRight = tester.GetBottomRight() == S.Full ? 1 : 0;

                return topLeft + top + topRight + left + right + bottomLeft + bottom + bottomRight >= 5
                    ? S.Empty
                    : value;
            }

            return value;
        }

        private static S TestChange(S[][] table, S value, int i, int j)
        {
            var iLimit = table.Length - 1;
            var jLimit = table[0].Length - 1;
            if (value == S.Empty)
            {
                // Top left
                var topLeft = i <= 0 || j <= 0 || table[i - 1][j - 1] != S.Full;
                // Top
                var top = i <= 0 || table[i - 1][j] != S.Full;
                // Top right
                var topRight = i <= 0 || j >= jLimit || table[i - 1][j + 1] != S.Full;
                // Left
                var left = j <= 0 || table[i][j - 1] != S.Full;
                // Right
                var right = j >= jLimit || table[i][j + 1] != S.Full;
                // Bottom left
                var bottomLeft = i >= iLimit || j <= 0 || table[i + 1][j - 1] != S.Full;
                // Bottom
                var bottom = i >= iLimit || table[i + 1][j] != S.Full;
                // Bottom right
                var bottomRight = i >= iLimit || j >= jLimit || table[i + 1][j + 1] != S.Full;

                return topLeft && top && topRight && left && right && bottomLeft && bottom && bottomRight
                    ? S.Full
                    : value;
            }

            if (value == S.Full)
            {
                // Top left
                var topLeft = i <= 0 || j <= 0 || table[i - 1][j - 1] != S.Full ? 0 : 1;
                // Top
                var top = i <= 0 || table[i - 1][j] != S.Full ? 0 : 1;
                // Top right
                var topRight = i <= 0 || j >= jLimit || table[i - 1][j + 1] != S.Full ? 0 : 1;
                // Left
                var left = j <= 0 || table[i][j - 1] != S.Full ? 0 : 1;
                // Right
                var right = j >= jLimit || table[i][j + 1] != S.Full ? 0 : 1;
                // Bottom left
                var bottomLeft = i >= iLimit || j <= 0 || table[i + 1][j - 1] != S.Full ? 0 : 1;
                // Bottom
                var bottom = i >= iLimit || table[i + 1][j] != S.Full ? 0 : 1;
                // Bottom right
                var bottomRight = i >= iLimit || j >= jLimit || table[i + 1][j + 1] != S.Full ? 0 : 1;

                return topLeft + top + topRight + left + right + bottomLeft + bottom + bottomRight >= 4
                    ? S.Empty
                    : value;
            }

            return value;
        }

        private static (S[][], int) ApplyChanges(S[][] table)
        {
            var newChart = CloneTable(table);
            var changeCount = 0;

            for (var i = 0; i < table.Length; i++)
            {
                var line = table[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var value = line[j];

                    var newValue = TestChange2(table, value, i, j);

                    if (newValue == value) continue;

                    newChart[i][j] = newValue;
                    changeCount++;
                }
            }

            return (newChart, changeCount);
        }

        public static void Puzzle(string[] lines)
        {
            var width = lines[0].Length;
            var height = lines.Length;

            var arr = new S[height][];

            // Initialize the chart
            for (var i = 0; i < height; i++)
            {
                arr[i] = new S[width];
                var line = lines[i].ToCharArray();
                for (var j = 0; j < width; j++)
                {
                    var c = line[j];
                    arr[i][j] = c switch
                    {
                        'L' => S.Empty,
                        '.' => S.Floor,
                        _ => S.Full
                    };
                }
            }

            var changeCount = -1;
            while (changeCount != 0)
            {
                var (newTable, nchangeCount) = ApplyChanges(arr);
                // Console.WriteLine("Changes: {0}", nchangeCount);
                // PrintTable(newTable);
                changeCount = nchangeCount;
                arr = newTable;
            }

            var count = CountOccupiedSeats(arr);
            Console.WriteLine("Occupied seats: {0}", count);
        }
    }
}