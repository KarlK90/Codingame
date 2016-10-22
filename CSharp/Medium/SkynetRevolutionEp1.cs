namespace Codingame.Medium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SkynetRevolutionEp1
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            bool[,] nodes = new bool[N, N];
            int[] gateways = new int[E];
            List<int> path = new List<int>();

            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                nodes[N1, N2] = true;
            }

            for (int i = 0; i < E; i++)
            {
                gateways[i] = int.Parse(Console.ReadLine()); // the index of a gateway gateways
            }

            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the gateways on which the Skynet agent is positioned this turn
                path = FindShortestPathToNode(nodes, N, gateways, SI);
                Console.WriteLine(path[0] + " " + path[1]);
                nodes[path[0], path[1]] = false;
            }
        }

        static List<int> FindShortestPathToNode(bool[,] nodes, int count, int[] gateways, int start)
        {
            int position = start;
            bool undiscoveredNodes = true;
            List<List<int>> paths = new List<List<int>>();

            Console.Error.WriteLine(count);

            for (int i = 0; i < count; i++)
            {

                Console.Error.WriteLine(i + " " + position);
                if (nodes[position, i])
                {
                    paths.Add(new List<int>() { position, i });
                    nodes[position, i] = false;
                }
            }

            while (undiscoveredNodes)
            {
                List<List<int>> newPaths = new List<List<int>>();

                foreach (var path in paths)
                {
                    position = path.Last();
                    List<int> childs = new List<int>();

                    for (int i = 0; i < count; i++)
                    {
                        if (nodes[position, i])
                        {
                            childs.Add(i);
                            nodes[position, i] = false;
                        }
                    }

                    if (childs.Count == 0)
                    {
                        continue;
                    }
                    if (childs.Count > 1)
                    {
                        for (int i = 0; i < childs.Count - 1; i++)
                        {
                            newPaths.Add(new List<int>(path) { childs[i] });
                        }
                    }
                    path.Add(childs.Last());
                }

                foreach (var path in newPaths)
                {
                    paths.Add(path);
                }

                foreach (var path in paths)
                {
                    if (gateways.Contains(path.Last())) return path;
                }

                if (nodes.Cast<bool>().All(x => x == false))
                {
                    undiscoveredNodes = false;
                }
            }
            return new List<int>();
        }
    }
}