namespace Codingame.Medium
{
    using System;
    using Codingame.Helper;

    using static Array2DExtensions;

    public class ShadowsOfTheKnightEp1
    {
        public static void Main(string[] args)
        {
            LineReader reader;
            if (args == null)
            {
                reader = new LineReader();
            }
            else
            {
                reader = new LineReaderMock(args);
            }

            string[] inputs;
            inputs = reader.ReadLine().Split(' ');

            bool[,] matrix = new bool[int.Parse(inputs[0]), int.Parse(inputs[1])]; // columns, rows
            int turns = int.Parse(reader.ReadLine()); // maximum number of turns before game over.

            inputs = reader.ReadLine().Split(' ');
            int[] position = { int.Parse(inputs[0]), int.Parse(inputs[1]) }; // column, row, zero-based
            int[] bombDir = new int[2];

            while (turns-- > 0)
            {
                bombDir = ParseDirection(reader.ReadLine());
                position = GetPossibleBombPosition(matrix, position, bombDir);

                Console.WriteLine(position[0] + " " + position[1]);
            }
        }

        // Optimized binary search algorithm, for finding the bomb. Possible location of the bomb is
        // narrowed in until there is only one possibility left. Every dimension of the 2D-grid is
        // handled separately. A cell that is marked as True is excluded from the search and acts as
        // a boundary, because in case the row or column counting function hits a True cell the count
        // is reset to zero. Therefore only False cells uninterupted by True Cells and directly
        // connected to the current position are counted, forming an area of interest. The new row or
        // column position is always in the middle of such an area.
        private static int[] GetPossibleBombPosition(bool[,] matrix, int[] pos, int[] dir)
        {
            if (dir[0] == -1)
            {
                matrix.SetColumn(pos[0], true);
                pos[0] -= (int)Math.Ceiling(
                    (double)Array2DExtensions.CountRowCells(
                        matrix,
                        row: pos[1],
                        start: 0,
                        end: pos[0],
                        predicate: CountCondition) / 2);
            }

            if (dir[0] == 1)
            {
                matrix.SetColumn(pos[0], true);
                pos[0] += (int)Math.Ceiling(
                    (double)Array2DExtensions.CountRowCells(
                        matrix,
                        row: pos[1],
                        start: matrix.GetLength(0) - 1,
                        end: pos[0],
                        predicate: CountCondition) / 2);
            }

            if (dir[1] == -1)
            {
                matrix.SetRow(pos[1], true);
                pos[1] -= (int)Math.Ceiling(
                    (double)Array2DExtensions.CountColumnCells(
                        matrix,
                        column: pos[0],
                        start: 0,
                        end: pos[1],
                        predicate: CountCondition) / 2);
            }

            if (dir[1] == 1)
            {
                matrix.SetRow(pos[1], true);
                pos[1] += (int)Math.Ceiling(
                    (double)Array2DExtensions.CountColumnCells(
                        matrix,
                        column: pos[0],
                        start: matrix.GetLength(1) - 1,
                        end: pos[1],
                        predicate: CountCondition) / 2);
            }

            return pos;
        }

        private static CompareResult CountCondition(bool cell, int index)
        {
            if (cell == true)
            {
                return CompareResult.Reset;
            }

            if (cell == false)
            {
                return CompareResult.True;
            }

            return CompareResult.False;
        }

        /// <summary>
        /// Parse the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L
        /// or UL). 1st dimension LR, 2nd dimension UD. Matrix point of origin (0,0) is in the upper
        /// left, therefore going up means decreasing 2nd dimension.
        /// </summary>
        private static int[] ParseDirection(string direction)
        {
            switch (direction)
            {
                case "U":
                    return new[] { 0, -1 };
                case "UR":
                    return new[] { 1, -1 };
                case "R":
                    return new[] { 1, 0 };
                case "DR":
                    return new[] { 1, 1 };
                case "D":
                    return new[] { 0, 1 };
                case "DL":
                    return new[] { -1, 1 };
                case "L":
                    return new[] { -1, 0 };
                case "UL":
                    return new[] { -1, -1 };
                default:
                    return new int[2];
            }
        }
    }

    public static class Array2DExtensions
    {
        public enum CompareResult
        {
            True,
            False,
            Reset
        }

        // Simple value comparion placeholder, eg. for break condition in for-loops
        delegate bool Compare<T>(T a, T b);

        public static T[,] SetColumns<T>(this T[,] matrix, int start, int end, T value)
        {
            int columnCellCount = matrix.GetLength(1);
            for (int i = start; i <= end; i++)
            {
                for (int j = 0; j < columnCellCount; j++)
                {
                    matrix[i, j] = value;
                }
            }

            return matrix;
        }

        public static T[,] SetColumn<T>(this T[,] matrix, int column, T value)
        {
            return SetColumns(matrix, column, column, value);
        }

        public static T[,] SetRows<T>(this T[,] matrix, int start, int end, T value)
        {
            int rowCellCount = matrix.GetLength(0);
            for (int i = start; i <= end; i++)
            {
                for (int j = 0; j < rowCellCount; j++)
                {
                    matrix[j, i] = value;
                }
            }

            return matrix;
        }

        public static T[,] SetRow<T>(this T[,] matrix, int column, T value)
        {
            return SetRows(matrix, column, column, value);
        }

        // Count every cell in a column matching predicate, start at 0.
        public static int CountColumnCells<T>(this T[,] matrix, int column, Func<T, int, CompareResult> predicate)
        {
            return CountColumnCells(matrix, column, 0, predicate);
        }

        // Count cells in a column matching predicate, beginning at start. 
        public static int CountColumnCells<T>(this T[,] matrix, int column, int start, Func<T, int, CompareResult> predicate)
        {
            return CountColumnCells(matrix, column, start, matrix.GetLength(1), predicate);
        }

        // Count cells in a column matching predicate, beginning at start until end.
        public static int CountColumnCells<T>(this T[,] matrix, int column, int start, int end, Func<T, int, CompareResult> predicate)
        {
            int count = 0;
            int step = 0;
            Compare<int> startEndCompare;

            // Handle backward iteration
            if (start > end)
            {
                startEndCompare = (s, e) => s > e;
                step = -1;
            }
            else
            {
                startEndCompare = (s, e) => s < e;
                step = 1;
            }

            for (int i = start; startEndCompare(i, end);  i += step)
            {
                switch (predicate(matrix[column, i], i))
                {
                    case CompareResult.True:
                        count++;
                        break;
                    case CompareResult.Reset:
                        count = 0;
                        break;
                    default:
                        break;
                }
            }

            return count;
        }
        
        // Count every cell in a row matching predicate, start at 0.
        public static int CountRowCells<T>(this T[,] matrix, int row, Func<T, int, CompareResult> predicate)
        {
            return CountRowCells(matrix, row, 0, predicate);
        }

        // Count cells in a row matching predicate, beginning at start. 
        public static int CountRowCells<T>(this T[,] matrix, int row, int start, Func<T, int, CompareResult> predicate)
        {
            return CountRowCells(matrix, row, start, matrix.GetLength(0), predicate);
        }

        // Count cells in a row matching predicate, beginning at start until end.
        public static int CountRowCells<T>(this T[,] matrix, int row, int start, int end, Func<T, int, CompareResult> predicate)
        {
            int count = 0;
            int step = 0;
            Compare<int> startEndCompare;

            // Handle backward iteration
            if (start > end)
            {
                startEndCompare = (s, e) => s > e;
                step = -1;
            }
            else
            {
                startEndCompare = (s, e) => s < e;
                step = 1;
            }

            for (int i = start; startEndCompare(i, end); i+= step)
            {
                switch (predicate(matrix[i, row], i))
                {
                    case CompareResult.True:
                        count++;
                        break;
                    case CompareResult.Reset:
                        count = 0;
                        break;
                    default:
                        break;
                }
            }

            return count;
        }
    }
}