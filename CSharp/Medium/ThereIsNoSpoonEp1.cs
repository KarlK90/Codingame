namespace Codingame.Medium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ThereIsNoSpoonEp1
    {
        public static void Main(string[] args)
        {
            int columns = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            int rows = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            List<int[]> nodes = ParseNodes(rows);

            foreach (var node in nodes)
            {
                var right = nodes.FirstOrDefault(x => x[1] == node[1] && x[0] > node[0]) ?? new[] { -1, -1 };
                var bottom = nodes.FirstOrDefault(x => x[0] == node[0] && x[1] > node[1]) ?? new[] { -1, -1 };
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", node[0], node[1], right[0], right[1], bottom[0], bottom[1]);
            }
        }

        private static List<int[]> ParseNodes(int rows)
        {
            List<int[]> nodes = new List<int[]>();

            for (int row = 0; row < rows; row++)
            {
                int col = 0;
                foreach (char c in Console.ReadLine())
                {
                    if (c == '0') nodes.Add(new[] { col, row });
                    col++;
                }
            }
            return nodes;
        }
    }
}