namespace Codingame.Easy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MIMEType
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
            int q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
            Dictionary<string, string> mimeTypes = new Dictionary<string, string>();

            while (n-- > 0)
            {
                string[] input = Console.ReadLine().Split(' ');
                mimeTypes.Add(input[0].ToLower(), input[1]);
            }

            while (q-- > 0)
            {
                string[] fname = Console.ReadLine().ToLower().Split('.');
                if (fname.Length < 2 || !mimeTypes.ContainsKey(fname.Last()))
                {
                    Console.WriteLine("UNKNOWN");
                    continue;
                }
                else
                {
                    Console.WriteLine(mimeTypes[fname.Last()]);
                }
            }
        }
    }
}