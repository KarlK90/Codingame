using System;
using System.Collections.Generic;
using System.Linq;

namespace Codingame.Medium
{
    public class BenderEP1
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int C = int.Parse(inputs[1]);

            List<List<char>> map = new List<List<char>>();
            for (int i = 0; i < L; i++)
            {
                map.Add(Console.ReadLine().ToCharArray().ToList());
            }

            int startL = map.FindIndex(l => l.Contains('@'));
            int startC = map[startL].FindIndex(c => c == '@');

            Bender firstBender = new Bender(startL, startC, Direction.SOUTH, State.NORMAL);
            Bender currentBender = firstBender.Clone() as Bender;

            List<string> way = new List<string>();

            do
            {
                var nextState = NextBender(currentBender, map);
                map = nextState.Item2;
                currentBender = nextState.Item1;
                way.Add(currentBender.Direction.ToString());
                Console.WriteLine(currentBender.Direction.ToString());
            } while (!currentBender.Equals(firstBender) || (currentBender.State & State.SUICIDE )!= State.SUICIDE);

            if ((currentBender.State & State.SUICIDE) != State.SUICIDE)
            {
                Console.WriteLine("LOOP");
            }
            else
            {
                way.ForEach(point => Console.WriteLine(point));
            }
        }

        static Tuple<Bender, List<List<char>>> NextBender(Bender currentBender, List<List<char>> map)
        {
            char modifier = map[currentBender.Line][currentBender.Column];

            switch (modifier)
            {
                case ' ':
                    return Tuple.Create(currentBender.Move(), map);
                case 'S':
                    return Tuple.Create(currentBender.Turn(Direction.SOUTH), map);
                case 'E':
                    return Tuple.Create(currentBender.Turn(Direction.EAST), map);
                case 'N':
                    return Tuple.Create(currentBender.Turn(Direction.NORTH), map);
                case 'W':
                    return Tuple.Create(currentBender.Turn(Direction.WEST), map);
                case '#':
                    return Tuple.Create(currentBender.Turn(), map);
                case 'B':
                    return Tuple.Create(currentBender.Breaker(), map);
                case 'X':
                    if ((currentBender.State & State.BREAKER) == State.BREAKER)
                    {
                        map[currentBender.Line][currentBender.Column] = ' ';
                        return Tuple.Create(currentBender.Move(), map);
                    }
                    return Tuple.Create(currentBender.Turn(), map);
                case '$':
                    return Tuple.Create(currentBender.Suicide(), map);
                case 'I':
                    return Tuple.Create(currentBender.Inverted(), map);
                case 'T':
                    var tempMap = map.ToList();
                    tempMap[currentBender.Line][currentBender.Column] = ' ';
                    int teleportL = tempMap.FindIndex(l => l.Contains('T'));
                    int teleportC = tempMap[teleportL].FindIndex(c => c == 'T');
                    return Tuple.Create(currentBender.Move(teleportL, teleportC), map);
                default:
                    return Tuple.Create(currentBender.Move(), map);
            }
        }

        class Bender : IEquatable<Bender>, ICloneable
        {
            public Bender(int line, int column, Direction direction, State state)
            {
                Line = line;
                Column = column;
                Direction = direction;
                State = state;
            }

            public int Line { get; }
            public int Column { get; }
            public Direction Direction { get; }
            public State State { get; }

            public bool Equals(Bender other)
            {
                return Line == other.Line && Column == other.Column && Direction == other.Direction && State == other.State;
            }

            public object Clone()
            {
                return new Bender(Line, Column, Direction, State);
            }

            public Bender Move()
            {
                int l = Line;
                int c = Column;

                switch (Direction)
                {
                    case Direction.SOUTH:
                        l = (State & State.INVERTED) != State.INVERTED ? l + 1 : l - 1;
                        break;
                    case Direction.EAST:
                        c = (State & State.INVERTED) != State.INVERTED ? c + 1 : c - 1;
                        break;
                    case Direction.WEST:
                        c = (State & State.INVERTED) != State.INVERTED ? c - 1 : c + 1;
                        break;
                    case Direction.NORTH:
                        l = (State & State.INVERTED) != State.INVERTED ? l - 1 : l + 1;
                        break;
                }

                return new Bender(l, c, Direction, State);
            }

            public Bender Turn()
            {
                return Turn(Direction);
            }

            public Bender Turn(Direction direction)
            {
                switch (direction)
                {
                    case Direction.SOUTH:
                        direction = (State & State.INVERTED) != State.INVERTED ? Direction.EAST : Direction.WEST;
                        break;
                    case Direction.EAST:
                        direction = (State & State.INVERTED) != State.INVERTED ? Direction.NORTH : Direction.SOUTH;
                        break;
                    case Direction.NORTH:
                        direction = (State & State.INVERTED) != State.INVERTED ? Direction.WEST : Direction.EAST;
                        break;
                    case Direction.WEST:
                        direction = (State & State.INVERTED) != State.INVERTED ? Direction.SOUTH : Direction.NORTH;
                        break;
                }

                return new Bender(Line, Column, direction, State);
            }

            public Bender Breaker()
            {
                State state = State;

                if ((State & State.BREAKER) == State.BREAKER)
                {
                    state &= ~State.BREAKER;
                }
                else
                {
                    state |= State.BREAKER;
                }

                return new Bender(Line, Column, Direction, state);
            }

            public Bender Suicide()
            {
                return new Bender(Line, Column, Direction, State.SUICIDE);
            }

            public Bender Inverted()
            {
                State state = State;

                if ((State & State.INVERTED) == State.INVERTED)
                {
                    state &= ~State.INVERTED;
                }
                else
                {
                    state |= State.INVERTED;
                }

                return new Bender(Line, Column, Direction, state);
            }

            public Bender Move(int l, int c)
            {
                return new Bender(l, c, Direction, State);
            }
        }

        enum Direction
        {
            SOUTH,
            EAST,
            NORTH,
            WEST
        }

        [Flags]
        enum State
        {
            NORMAL,
            BREAKER,
            SUICIDE,
            INVERTED
        }
    }
}