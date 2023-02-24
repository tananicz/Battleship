using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class ShipBoard
    {
        private enum ShipBoardCube { Ship, Margin, Nothing };

        private enum ShipDirection { Horizontal, Vertical };

        private struct Segment
        {
            public int Begin { get; set; }
            public int End { get; set; }
            public int Length { get { return End - Begin + 1; } }
        }

        private ShipBoardCube[,] shipsBoard = new ShipBoardCube[GameConfig.BoardSize, GameConfig.BoardSize];
        private Random random = new Random();

        public ShipBoard()
        {
            for (int row = 0; row < GameConfig.BoardSize; row++)
            {
                for (int col = 0; col < GameConfig.BoardSize; col++)
                {
                    shipsBoard[col, row] = ShipBoardCube.Nothing;
                }
            }

            DistributeShips();
        }

        public bool IsShip(int x, int y)
        {
            return shipsBoard[x, y] == ShipBoardCube.Ship;
        }

        private void DistributeShips()
        {
            for (int i = 0; i < GameConfig.ShipSizes.Length; i++)
            {
                int shipSize = GameConfig.ShipSizes[i];
                ShipDirection shipDirection = random.Next(2) == 0 ? ShipDirection.Horizontal : ShipDirection.Vertical;
                bool shipPlaced = false;
                bool tryOtherDirection = false;
                bool useMargins = true;

                do
                {
                    List<int> availableLines = new List<int>();
                    for (int j = 0; j < GameConfig.BoardSize; j++)
                        availableLines.Add(j);

                    while (availableLines.Count > 0)
                    {
                        int lineNum = availableLines[random.Next(availableLines.Count)];
                        availableLines.Remove(lineNum);

                        List<Segment> validSegments = FindValidSegments(lineNum, shipDirection, shipSize, useMargins);

                        if (validSegments.Count > 0)
                        {
                            Segment segment = validSegments[random.Next(validSegments.Count)];
                            int initialPosition = segment.Begin + random.Next(segment.Length - shipSize + 1);

                            PlaceShipOnBoard(initialPosition, lineNum, shipSize, shipDirection, useMargins);

                            shipPlaced = true;
                            break;
                        }

                        if (availableLines.Count == 0 && !tryOtherDirection && useMargins)
                        {
                            /*
                            If we're here then during the 1st iteration, when tryOtherDirection is false and useMargins is true, we searched 
                            for free space in all available rows (or columns) with no result. So we change ship direction and in the next iteration 
                            we'll search all available columns (or rows respectively), indicating that fact by setting tryOtherDirection to true.
                            */

                            tryOtherDirection = true;
                            shipDirection = shipDirection == ShipDirection.Horizontal ? ShipDirection.Vertical : ShipDirection.Horizontal;
                        }
                        else if (availableLines.Count == 0 && tryOtherDirection && useMargins)
                        {
                            /*
                            If we're here then during the 2nd iteration, when tryOtherDirection is true and useMargins is true, we searched 
                            for free space in all available rows (or columns) with no result. For the next iteration we set tryOtherDirection 
                            and placeHorizontally variables just like they were at the beginning, this time however useMargins will be set to false 
                            in order to ignore spaces around ships.
                            */

                            tryOtherDirection = false;
                            shipDirection = shipDirection == ShipDirection.Horizontal ? ShipDirection.Vertical : ShipDirection.Horizontal;
                            useMargins = false;
                        }
                        else if (availableLines.Count == 0 && !tryOtherDirection && !useMargins)
                        {
                            /*
                            If we're here then during the 3rd iteration, when tryOtherDirection is false and useMargins is false, we searched 
                            for free space in all available rows (or columns) with no result. For the next iteration we change ship direction,
                            indicating that fact by setting tryOtherDirection to true, and search all available columns (or rows respectively).
                            Use margins is still set to false - we allow ignoring spaces around ships.
                            */

                            tryOtherDirection = true;
                            shipDirection = shipDirection == ShipDirection.Horizontal ? ShipDirection.Vertical : ShipDirection.Horizontal;
                        }
                        else if (availableLines.Count == 0 && tryOtherDirection && !useMargins)
                        {
                            /*
                            If we're here then we went through all 4 possible modes of searching, i.e. setting tryOtherDirection to true/false
                            and useMargins to true/false. Our search for free space to place a ship failed.
                            */

                            throw new Exception("Ooops! No room for all the ships on board!");
                        }
                    }
                } while (!shipPlaced);
            }
        }

        private void PlaceShipOnBoard(int initialPosition, int lineNum, int shipSize, ShipDirection shipDirection, bool useMargins)
        {
            if (shipDirection == ShipDirection.Horizontal)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (useMargins && i == 0 && initialPosition - 1 >= 0)
                        shipsBoard[initialPosition - 1, lineNum] = ShipBoardCube.Margin;

                    shipsBoard[initialPosition + i, lineNum] = ShipBoardCube.Ship;
                    if (useMargins)
                    {
                        if (lineNum > 0)
                            shipsBoard[initialPosition + i, lineNum - 1] = ShipBoardCube.Margin;
                        if (lineNum < GameConfig.BoardSize - 1)
                            shipsBoard[initialPosition + i, lineNum + 1] = ShipBoardCube.Margin;
                    }

                    if (useMargins && (i == shipSize - 1) && (initialPosition + i + 1 < GameConfig.BoardSize))
                        shipsBoard[initialPosition + i + 1, lineNum] = ShipBoardCube.Margin;
                }
            }
            else
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (useMargins && i == 0 && initialPosition - 1 >= 0)
                        shipsBoard[lineNum, initialPosition - 1] = ShipBoardCube.Margin;

                    shipsBoard[lineNum, initialPosition + i] = ShipBoardCube.Ship;
                    if (useMargins)
                    {
                        if (lineNum > 0)
                            shipsBoard[lineNum - 1, initialPosition + i] = ShipBoardCube.Margin;
                        if (lineNum < GameConfig.BoardSize - 1)
                            shipsBoard[lineNum + 1, initialPosition + i] = ShipBoardCube.Margin;
                    }

                    if (useMargins && (i == shipSize - 1) && (initialPosition + i + 1 < GameConfig.BoardSize))
                        shipsBoard[lineNum, initialPosition + i + 1] = ShipBoardCube.Margin;
                }
            }
        }

        private List<Segment> FindValidSegments(int lineNum, ShipDirection shipDirection, int shipSize, bool useMargins)
        {
            List<Segment> segments = new List<Segment>();

            bool countStarted = false;
            Segment segment = new Segment();
            ShipBoardCube tmpCube;
            bool lineElement;

            for (int i = 0; i < GameConfig.BoardSize; i++)
            {
                tmpCube = shipDirection == ShipDirection.Horizontal ? shipsBoard[i, lineNum] : shipsBoard[lineNum, i];

                if (useMargins)
                {
                    //in this case lineElement will be true if tmpCube is pointing to Ship or Margin
                    lineElement = !(tmpCube == ShipBoardCube.Nothing);
                }
                else
                {
                    //in this case lineElement will be true if tmpCube is pointing to Ship only
                    lineElement = tmpCube == ShipBoardCube.Ship;
                }

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
