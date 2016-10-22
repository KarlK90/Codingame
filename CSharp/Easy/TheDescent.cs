namespace Codingame.Easy
{
    using System;

    public class TheDescent
    {
        static void Main(string[] args)
        {

            // game loop
            while (true)
            {
                int[] highestMountain = new int[] { 0, 0 };

                for (int i = 0; i < 8; i++)
                {
                    int mountainH = int.Parse(Console.ReadLine()); // represents the height of one mountain, from 9 to 0.

                    if (mountainH > highestMountain[0])
                    {
                        highestMountain[0] = mountainH;
                        highestMountain[1] = i;
                    }
                }

                Console.WriteLine(highestMountain[1]); // The number of the mountain to fire on.
            }
        }
    }
}