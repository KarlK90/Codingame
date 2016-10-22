namespace Codingame.Easy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HorseRacing
    {
        public static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            List<int> horses = new List<int>();

            while (N-- > 0)
            {
                horses.Add(int.Parse(Console.ReadLine()));
            }

            horses = horses.OrderBy(n => n).ToList();

            int min = horses.Select((element, index) =>
            {
                int count = horses.Count();
                int? a = null;
                int? b = null;
                if (index + 1 < count)
                {
                    a = Math.Abs(horses.ElementAt(index) - horses.ElementAt(index + 1));
                }
                if (index - 1 >= 0)
                {
                    b = Math.Abs(horses.ElementAt(index) - horses.ElementAt(index - 1));
                }
                if (!a.HasValue) return b.Value;
                if (!b.HasValue) return a.Value;
                return Math.Min(a.Value, b.Value);
            }).Min();

            Console.WriteLine(min);
        }
    }
}