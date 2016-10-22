using System;

namespace Codingame.Easy
{
    public class ChuckNorris
    {
        public static void Main(string[] args)
        {
            char[] message = Console.ReadLine().ToCharArray();
            string result = string.Empty;

            foreach (var c in message)
            {
                result += Convert.ToString(c, 2).PadLeft(7, '0');
            }

            Console.Write(ToChuck(result));
        }

        public static string ToChuck(string bin)
        {
            bin += "A";
            string type = string.Empty;
            string result = string.Empty;
            string amount = string.Empty;

            foreach (char d in bin)
            {
                switch (d)
                {
                    case '0':
                        if (type == "0" || type == string.Empty)
                        {
                            type = "0";
                            amount += '0';
                        }
                        else
                        {
                            type = "0";
                            result += "0 " + amount + " ";
                            amount = "0";
                        }

                        break;
                    case '1':
                        if (type == "1" || type == string.Empty)
                        {
                            type = "1";
                            amount += '0';
                        }
                        else
                        {
                            type = "1";
                            result += "00 " + amount + " ";
                            amount = "0";
                        }

                        break;
                    default:
                        if (type == "1")
                        {
                            result += "0 " + amount;
                        }
                        else
                        {
                            result += "00 " + amount;
                        }

                        return result;
                }
            }

            return result;
        }
    }
}