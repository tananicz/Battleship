using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Player
    {
        internal enum GameBoardCube { None, Hit, Miss };

        private struct Position
        {
            public int Column { get; set; }
            public int Row { get; set; }
        }

        private GameBoardCube[,] gameBoard = new GameBoardCube[GameConfig.BoardSize, GameConfig.BoardSize];
        private ShipBoard shipBoard = new ShipBoard();
        private HashSet<Position> shots = new HashSet<Position>();
        private Random random = new Random();

        public bool HasShip(int x, int y)
        {
            return shipBoard.IsShip(x, y);
        }

        public Player()
        {
            for (int x = 0; x < GameConfig.BoardSize; x++)
                for (int y = 0; y < GameConfig.BoardSize; y++)
                    gameBoard[x, y] = GameBoardCube.None;
        }
    }
}
