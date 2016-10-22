namespace Codingame.Easy
{
    using System;
    using System.Text.RegularExpressions;

    public class AsciiArt
    {
        public static void Main(string[] args)
        {
            int l = int.Parse(Console.ReadLine());
            int h = int.Parse(Console.ReadLine());
            string t = Console.ReadLine();

            string input = normalizeInputString(t);
            string[][] characters = parseAsciiChars(l, h);

            string output = string.Empty;

            for (int r = 0; r < h; r++)
            {
                foreach (char c in input)
                {
                    if (c == 63)
                    { // Handle "?" 
                        output += characters[26][r];
                    }
                    else
                    {        // Handle A-Z       
                        output += characters[c - 65][r];
                    }

                }

                output += "\n";
            }

            Console.WriteLine(output);
        }

        private static string[][] parseAsciiChars(int L, int H)
        {
            string[][] characters = new string[27][];
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i] = new string[H];
            }

            for (int r = 0; r < H; r++)
            {
                string ROW = Console.ReadLine();
                int count = 0;
                for (int c = 0; c < ROW.Length; count++, c = count * L)
                {
                    characters[count][r] += ROW.Substring(c, L);
                }
            }
            return characters;
        }

        private static string normalizeInputString(string input)
        {
            Regex rgx = new Regex("([^A-Z\\?])");
            input = input.ToUpper();
            return rgx.Replace(input, "?");
        }
    }
}