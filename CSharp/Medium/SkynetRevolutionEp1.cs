namespace Codingame.Medium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SkynetRevolutionEp1
    {
        public static void Main(string[] args)
        {
            LineReader reader;
            if (args == null)
            {
                reader = new LineReader();
            }
            else
            {
                reader = new LineReaderMock(args);
            }

            string[] input = reader.ReadLine().Split(' ');
            int nodeCount = int.Parse(input[0]); // the total number of nodes in the level, including the gateways
            int linkCount = int.Parse(input[1]); // the number of links
            int gatewayCount = int.Parse(input[2]); // the number of exit gateways

            bool[,] nodes = ParseNodes(linkCount, nodeCount, reader);
            int[] gateways = ParseGateways(gatewayCount, reader);

            // game loop
            while (true)
            {
                int startNode = int.Parse(reader.ReadLine()); // The index of the gateways on which the Skynet agent is positioned this turn
                List<int> path = FindShortestPathToNode(nodes, gateways, startNode);
                Console.WriteLine(path[0] + " " + path[1]);
                nodes[path[0], path[1]] = false;
                nodes[path[1], path[0]] = false;

            }
        }

        public static List<int> FindShortestPathToNode(bool[,] nodesArgs, int[] gateways, int startNode)
        {
            int position = startNode;
            bool undiscoveredNodes = true;
            int nodeCount = nodesArgs.GetLength(0);

            // Don't mutate passed nodes array
            bool[,] nodes = (bool[,])nodesArgs.Clone();

            List<List<int>> solutionPaths = new List<List<int>>();
            List<List<int>> paths = new List<List<int>>();

            for (int i = 0; i < nodeCount; i++)
            {
                if (nodes[position, i])
                {
                    paths.Add(new List<int> { position, i });
                    nodes[position, i] = false;
                    nodes[i, position] = false;
                }
            }

            while (undiscoveredNodes)
            {
                List<List<int>> newPaths = new List<List<int>>();

                foreach (var path in paths)
                {
                    position = path.Last();
                    List<int> childs = new List<int>();

                    for (int i = 0; i < nodeCount; i++)
                    {
                        if (nodes[position, i])
                        {
                            childs.Add(i);
                            nodes[position, i] = false;
                            nodes[i, position] = false;
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
                            // clone path and append child node
                            newPaths.Add(new List<int>(path) { childs[i] });
                        }
                    }

                    path.Add(childs.Last());
                }

                paths.AddRange(newPaths);

                paths.ForEach(p =>
                {
                    if (p.Exists(gateways.Contains))
                    {
                        solutionPaths.Add(p);
                    }
                });

                if (solutionPaths.Count > 0)
                {
                    // Return the shortest path, by generating a list of paths trimmed to exit nodes, sorting them by lenght and taking the first.
                    return solutionPaths.Select(p => p.Take(gateways.Select(g => p.IndexOf(g)).Where(i => i > 0).Min() + 1))
                        .OrderBy(s => s.Count())
                        .First()
                        .ToList();
                }

                if (nodes.Cast<bool>().All(x => x == false))
                {
                    undiscoveredNodes = false;
                }
            }

            return new List<int>();
        }

        private static bool[,] ParseNodes(int linkCount, int nodeCount, LineReader reader)
        {
            bool[,] nodes = new bool[nodeCount, nodeCount];

            for (int i = 0; i < linkCount; i++)
            {
                string[] input = reader.ReadLine().Split(' ');
                int node1 = int.Parse(input[0]); // N1 and N2 defines a link between these nodes
                int node2 = int.Parse(input[1]);
                nodes[node1, node2] = true;
                nodes[node2, node1] = true;
            }

            return nodes;
        }

        private static int[] ParseGateways(int gatewayCount, LineReader reader)
        {
            int[] gateways = new int[gatewayCount];

            for (int i = 0; i < gatewayCount; i++)
            {
                gateways[i] = int.Parse(reader.ReadLine()); // the index of a gateway gateways
            }

            return gateways;
        }
    }

    public class LineReader
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    public class LineReaderMock : LineReader
    {
        public LineReaderMock(string[] input)
        {
            this.input = input;
        }

        private int calls = 0;
        private string[] input;

        public override string ReadLine()
        {
            string ret = input[calls];
            calls++;
            return ret;
        }
    }
}