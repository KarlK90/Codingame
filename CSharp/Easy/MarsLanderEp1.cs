namespace Codingame.Easy
{
    using System;

    public class MarsLanderEp1
    {
        static void Main(string[] args)
        {
            string[] inputs;
            int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            }


            int thrust = 0;

            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.

                if (vSpeed < -40 && thrust < 4)
                {
                    thrust++;
                }

                if (vSpeed > -15 && thrust > 0)
                {
                    thrust--;
                }

                Console.WriteLine("0 " + thrust);
            }
        }
    }
}