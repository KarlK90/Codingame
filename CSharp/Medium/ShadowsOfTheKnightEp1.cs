namespace Codingame.Medium
{
    using System;
    using System.Runtime.Remoting.Messaging;

    public class ShadowsOfTheKnightEp1
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');

            bool[,] matrix = new bool[int.Parse(inputs[0]), int.Parse(inputs[1])]; // rows, columns
            int turns = int.Parse(Console.ReadLine()); // maximum number of turns before game over.

            inputs = Console.ReadLine().Split(' ');
            int[] position = { int.Parse(inputs[0]), int.Parse(inputs[1]) }; // row, column
            
            while (true)
            {
                var bombDir = ParseDirection(Console.ReadLine());
                SearchBomb(matrix, position, bombDir, turns);
                Console.WriteLine("0 0");
                turns--;
            }
        }

        static void SearchBomb(bool[,] matrix, int[] position, int[] direction, int turns)
        {
            if (direction[0] == -1)
            {

            }

            if (direction[0] == 1)
            {
                
            }

            if (direction[1] == -1)
            {
                
            }

            if (direction[1] == 1)
            {
                
            }
        }

        /// <summary>
        /// Parse the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
        /// </summary>
        static int[] ParseDirection(string direction)
        {
            switch (direction)
            {
                case "U":
                    return new[] { 0, 1 };
                case "UR":
                    return new[] { 1, 1 };
                case "R":
                    return new[] { 1, 0 };
                case "DR":
                    return new[] { 1, -1 };
                case "D":
                    return new[] { 0, -1 };
                case "DL":
                    return new[] { -1, -1 };
                case "L":
                    return new[] { -1, 0 };
                case "UL":
                    return new[] { -1, 1 };
                default:
                    return new int[2];
            }
        }
    }
}