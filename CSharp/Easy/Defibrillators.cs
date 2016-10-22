namespace Codingame.Easy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Defibrillators
    {
        public static void Main(string[] args)
        {
            double lon = double.Parse(Console.ReadLine().Replace(",", "."));
            double lat = double.Parse(Console.ReadLine().Replace(",", "."));
            int N = int.Parse(Console.ReadLine());
            var defibs = new List<Tuple<int, string, string, string, double, double>>();

            while (N-- > 0)
            {
                string[] defib = Console.ReadLine().Split(';');
                defibs.Add(Tuple.Create(
                        int.Parse(defib[0]),
                        defib[1],
                        defib[2],
                        defib[3],
                        double.Parse(defib[4].Replace(",", ".")),
                        double.Parse(defib[5].Replace(",", "."))));
            }

            var result = defibs.Select(defib => new { name = defib.Item2, dist = Distance(defib.Item6, defib.Item5, lat, lon) }).OrderBy(d => d.dist).First();

            Console.WriteLine(result.name);
        }

        public static double Distance(double lat1, double long1, double lat2, double long2)
        {
            double x = (long2 - long1) * Math.Cos((lat1 + lat2) / 2);
            double y = lat2 - lat1;
            return Math.Sqrt(x * x + y * y) * 6371;
        }
    }
}