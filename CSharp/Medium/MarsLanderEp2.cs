namespace Codingame.Medium
{
    using System;
    using System.Collections.Generic;

    public class MarsLanderEp2
    {
        private const double MARSGRAV = 3.711;
        private const int T = 1;

        private static Point startPoint;
        private static Point landingPoint;
        private static Point curvePoint;
        private static Point referencePoint;

        static void Main(string[] args)
        {
            string[] inputs;

            var surface = new List<int[]>();
            var surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
            
            while (surfaceN-- > 0)
            {
                inputs = Console.ReadLine().Split(' ');
                Console.Error.WriteLine("{0} {1}", inputs[0], inputs[1]);
                // 0 = X coordinate of a surface point. (0 to 6999), 1 = Y coordinate of a surface point. By
                // linking all the points together in a sequential fashion, you form the surface of Mars.
                surface.Add(new[] { int.Parse(inputs[0]), int.Parse(inputs[1]) });
            }

            int[] landingArea = GetLandingArea(surface); // Landing hight, start, end
            Console.Error.WriteLine("{0} {1} {2}", landingArea[0], landingArea[1], landingArea[2]);

            referencePoint = new Point();
            landingPoint = new Point((landingArea[1] + landingArea[2]) / 2, landingArea[0]);

            double x, y, tX = 0, deviationX, deviationY, hSpeed, vSpeed, fuel, rotate, power;
            double dY, dX, powerY, powerX, testPowerY, testPowerX;
            
            // game loop
            while (true)
            {
                ParseGameArguments(out x, out y, out hSpeed, out vSpeed, out fuel, out rotate, out power);
                CheckPathPoints(x, y);

                tX = CalculateTx(x);
                referencePoint = MarsMath.QuadraticBezier(startPoint, curvePoint, landingPoint, tX);
                deviationX = x - referencePoint.x;
                deviationY = y - referencePoint.y;

                // Current acceleration vector distribution
                powerY = MarsMath.Sin(90 - rotate) * power;
                powerX = MarsMath.Cos(90 - rotate) * power;

                // Next possition difference of Mars Lander
                dY = ((MARSGRAV - powerY) / 2) + vSpeed;
                dX = hSpeed + powerX;

                testPowerY = MARSGRAV;

                if (deviationY >= 20)
                {
                    testPowerY = 0;
                }

                if (deviationY <= -20)
                {
                    testPowerY = 3.99;
                }

                testPowerX = Math.Sqrt(Math.Pow(4, 2) - Math.Pow(testPowerY, 2));

                Console.Error.WriteLine("testPowerX: {0}", testPowerX);

                rotate = MarsMath.Asin(testPowerX / 4);

                if (deviationX < 0)
                {
                    rotate *= -1;
                }

                Console.Error.WriteLine("rotate: {0} power: {1}", rotate, power);
                Console.Error.WriteLine("Deviation X: {0} Deviation Y: {1}", deviationX, deviationY);
                Console.Error.WriteLine("Yd:{0} Y: {1} PowerY: {2}", dY, y + dY, powerY);
                Console.Error.WriteLine("Xd:{0} X: {1} PowerX: {2}", dX, x + dX, powerX);

                Console.WriteLine("{0:0} 4", rotate);
            }
        }

        private static double CalculateTx(Double x)
        {
            double tX = (x - startPoint.x) / (landingPoint.x - startPoint.x);
            tX = tX < 0 ? 0 : tX;
            tX = tX > 1 ? 1 : tX;
            return tX;
        }

        private static void CheckPathPoints(double x, double y)
        {
            if (startPoint == null)
            {
                startPoint = new Point(x, y);
            }

            if (curvePoint == null)
            {
                curvePoint = new Point(landingPoint.x, startPoint.y);
            }
        }

        private static int[] GetLandingArea(List<int[]> surface)
        {
            var landingArea = new int[3];

            for (var i = 0; i < surface.Count; i++)
            {
                if (surface[i][1] == surface[i + 1][1])
                {
                    landingArea[0] = surface[i][1];
                    landingArea[1] = surface[i][0];
                    landingArea[2] = surface[i + 1][0];
                    break;
                }
            }

            return landingArea;
        }

        private static void ParseGameArguments(
            out double x,
            out double y,
            out double hSpeed,
            out double vSpeed,
            out double fuel,
            out double rotate,
            out double power)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            x = double.Parse(inputs[0]);
            y = double.Parse(inputs[1]);
            hSpeed = double.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            vSpeed = double.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            fuel = double.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            rotate = double.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            power = double.Parse(inputs[6]); // the thrust power (0 to 4).
        }
    }

    public static class MarsMath
    {
        public static Point QuadraticBezier(Point A, Point B, Point C, double t)
        {
            return new Point(
                Math.Pow(1 - t, 2) * A.x + 2 * (1 - t) * t * B.x + Math.Pow(t, 2) * C.x,
                Math.Pow(1 - t, 2) * A.y + 2 * (1 - t) * t * B.y + Math.Pow(t, 2) * C.y);
        }

        private static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }

        public static double Sin(double deg)
        {
            return Math.Sin(deg * Math.PI / 180);
        }
        public static double Cos(double deg)
        {
            return Math.Cos(deg * Math.PI / 180);
        }
        public static double Asin(double sin)
        {
            return Math.Asin(sin) * (180 / Math.PI);
        }
        public static double Acos(double cos)
        {
            return Math.Acos(cos) * (180 / Math.PI);
        }
    }

    public class Point
    {
        public Point()
        {
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double x;
        public double y;
    }
}