using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal enum GameBoardCube { None, Hit, Miss };

    internal class Player
    {
        private struct Shot
        {
            public int Column { get; set; }
            public int Row { get; set; }
        }

        private GameBoardCube[,] gameBoard = new GameBoardCube[GameConfig.BoardSize, GameConfig.BoardSize];
        private ShipBoard shipBoard = new ShipBoard();
        private List<Shot> shots = new List<Shot>();
        private Random random = new Random();
        private int hitsCount = 0;
        
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;

            for (int row = 0; row < GameConfig.BoardSize; row++)
            {
                for (int col = 0; col < GameConfig.BoardSize; col++)
                {
                    gameBoard[col, row] = GameBoardCube.None;
                    shots.Add(new Shot { Column = col, Row = row });
                }
            }
        }

        public bool HasShip(int x, int y)
        {
            return shipBoard.IsShip(x, y);
        }

        public GameBoardCube GetGameBoardCubeAt(int row, int col)
        {
            return gameBoard[row, col];
        }

        public bool IsWinner()
        {
            return hitsCount == GameConfig.TotalShipsShots;
        }

        public string ShootAt(Player opponent)
        {
            StringBuilder sb = new StringBuilder(Name);

            if (shots.Count > 0)
            {
                Shot shot = shots[random.Next(shots.Count)];
                shots.Remove(shot);

                if (opponent.HasShip(shot.Column, shot.Row))
                {
                    gameBoard[shot.Column, shot.Row] = GameBoardCube.Hit;
                    hitsCount++;
                    sb.Append($" fired a gun at grid ({shot.Column + 1}, {shot.Row + 1}) and HIT {opponent.Name}'s ship!!! ");
                }
                else
                {
                    gameBoard[shot.Column, shot.Row] = GameBoardCube.Miss;
                    sb.Append($" fired a gun at grid ({shot.Column + 1}, {shot.Row + 1}) and missed ;-( ");
                }
            }
            else
                sb.Append(" has no shots left anymore... ");

            return sb.ToString();
        }
    }
}
