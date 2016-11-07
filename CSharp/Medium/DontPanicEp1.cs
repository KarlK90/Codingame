namespace Codingame.Medium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DontPanicEp1
    {
        static void Main(string[] args)
        {
            var elevators = new Dictionary<int, int>();

            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            int exitPos = int.Parse(inputs[4]); // position of the exit on its floor

            elevators.Add(exitFloor, exitPos);

            int nbElevators = int.Parse(inputs[7]); // number of elevators
            while (nbElevators-- > 0)
            {
                inputs = Console.ReadLine().Split(' ');
                int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                elevators.Add(elevatorFloor, elevatorPos);
            }

            int goal;
            string[] idle = { "-1", "-1", "NONE" };

            while (true)
            {
                inputs = Console.ReadLine().Split(' ');

                if (inputs.SequenceEqual(idle))
                {
                    Console.WriteLine("WAIT");
                    continue;
                }

                int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT

                elevators.TryGetValue(cloneFloor, out goal);

                if (clonePos <= goal && direction == "RIGHT")
                {
                    Console.WriteLine("WAIT");
                }

                if (clonePos < goal && direction == "LEFT")
                {
                    Console.WriteLine("BLOCK");
                }

                if (clonePos > goal && direction == "RIGHT")
                {
                    Console.WriteLine("BLOCK");
                }

                if (clonePos >= goal && direction == "LEFT")
                {
                    Console.WriteLine("WAIT");
                }
            }
        }
    }
}