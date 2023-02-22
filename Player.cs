using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal enum BoardCube { None, Hit, Miss };

    internal class Player
    {
        private struct Position
        {
            public int Column { get; set; }
            public int Row { get; set; }
        }

        private struct Segment
        {
            public int Begin { get; set; }
            public int End { get; set; }
            public int Length { get { return End - Begin + 1; } }
        }

        private BoardCube[,] gameBoard = new BoardCube[GameConfig.BoardSize, GameConfig.BoardSize];
        private Boolean[,] shipsBoard = new Boolean[GameConfig.BoardSize, GameConfig.BoardSize];
        private HashSet<Position> shots = new HashSet<Position>();
        private Random random = new Random();

        public bool IsShip(int x, int y)
        {
            return shipsBoard[x, y];
        }

        public Player()
        {
            for (int x = 0; x < GameConfig.BoardSize; x++)
                for (int y = 0; y < GameConfig.BoardSize; y++)
                { 
                    gameBoard[x, y] = BoardCube.None;
                    shipsBoard[x, y] = false;
                }

            PlaceShips();
        }

        private void PlaceShips()
        {
            for (int i = 0; i < GameConfig.ShipSizes.Length; i++)
            {
                int shipSize = GameConfig.ShipSizes[i];
                bool placeHorizontally = random.Next(2) == 0;
                bool shipPlaced = false;
                bool tryOtherDirection = false;

                do
                {
                    List<int> availableLines = new List<int>();
                    for (int j = 0; j < GameConfig.BoardSize; j++)
                        availableLines.Add(j);

                    while (availableLines.Count > 0)
                    {
                        int lineNum = availableLines[random.Next(availableLines.Count)];
                        availableLines.Remove(lineNum);

                        List<Segment> validSegments = FindValidSegments(lineNum, placeHorizontally, shipSize);

                        if (validSegments.Count > 0)
                        {
                            Segment segment = validSegments[random.Next(validSegments.Count)];
                            int initialPosition = segment.Begin + random.Next(segment.Length - shipSize + 1);

                            for (int j = 0; j < shipSize; j++)
                            {
                                if (placeHorizontally)
                                    shipsBoard[initialPosition + j, lineNum] = true;
                                else
                                    shipsBoard[lineNum, initialPosition + j] = true;
                            }

                            shipPlaced = true;
                            break;
                        }

                        if (availableLines.Count == 0 && !tryOtherDirection)
                        {
                            tryOtherDirection = true;
                            placeHorizontally = !placeHorizontally;
                        }
                        else if (availableLines.Count == 0 && tryOtherDirection)
                            throw new Exception("Ooops! No room for all the ships on board!");
                    }
                } while (!shipPlaced);
            }
        }

        private List<Segment> FindValidSegments(int lineNum, bool isHorizontal, int shipSize)
        {
            List<Segment> segments = new List<Segment>();

            bool countStarted = false;
            Segment segment = new Segment();
            bool lineElement;

            for (int i = 0; i < GameConfig.BoardSize; i++)
            {
                lineElement = isHorizontal ? shipsBoard[i, lineNum] : shipsBoard[lineNum, i];

                if (countStarted)
                {
                    if (lineElement)
                    {
                        countStarted = false;
                        segment.End = i - 1;
                        if (segment.End - segment.Begin + 1 >= shipSize)
                            segments.Add(segment);
                    }
                }
                else
                {
                    if (!lineElement)
                    {
                        countStarted = true;
                        segment = new Segment();
                        segment.Begin = i;
                    }
                }

                if (i == GameConfig.BoardSize - 1 && countStarted && !lineElement)
                {
                    segment.End = i;
                    if (segment.End - segment.Begin + 1 >= shipSize)
                        segments.Add(segment);
                }
            }

            return segments;
        }
    }
}
