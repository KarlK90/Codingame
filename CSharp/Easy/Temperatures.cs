namespace Codingame.Easy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Temperatures
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
            List<int> data = new List<int>();

            string[] input = Console.ReadLine().Split(' ');
            if (input.Length == 1)
            {
                if (input[0] == string.Empty) { Console.WriteLine(0); } else { Console.WriteLine(input[0]); };
                return;
            }

            foreach (string temp in input)
            {
                data.Add(int.Parse(temp));
            }

            data.Add(0);
            data = data.OrderBy(t => t).ToList();
            int index = data.IndexOf(0);

            if (index == data.Count() - 1) Console.WriteLine(data[index - 1]);
            else if (index == 0) Console.WriteLine(data[index + 1]);
            else if (Math.Abs(data[index - 1]) < Math.Abs(data[index + 1])) Console.WriteLine(data[index - 1]);
            else if (Math.Abs(data[index - 1]) > Math.Abs(data[index + 1])) Console.WriteLine(data[index + 1]);
            else if (Math.Abs(data[index - 1]) == Math.Abs(data[index + 1])) Console.WriteLine(data[index + 1]);
        }
    }
}