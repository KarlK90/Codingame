namespace Codingame.Easy
{
    using System;

    public class PowerOfThorEp1
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int lightX = int.Parse(inputs[0]); // the X position of the light of power
            int lightY = int.Parse(inputs[1]); // the Y position of the light of power
            int thorX = int.Parse(inputs[2]); // Thor's starting X position
            int thorY = int.Parse(inputs[3]); // Thor's starting Y position
            string directionX = string.Empty;
            string directionY = string.Empty;

            // game loop
            while (true)
            {
                int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.

                directionY = string.Empty;
                directionX = string.Empty;

                if (lightY < thorY)
                {
                    directionY = "N";
                    thorY--;
                }

                if (lightY > thorY)
                {
                    directionY = "S";
                    thorY++;
                }


                if (lightX < thorX)
                {
                    directionX = "W";
                    thorX--;
                }

                if (lightX > thorX)
                {
                    directionX = "E";
                    thorX++;
                }

                Console.WriteLine(directionY + directionX);
            }
        }
    }
}