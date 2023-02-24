using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    internal class GraphicsPainter
    {
        private Graphics graphics;
        private TextBox messageTextBox;
        private Font boardFont;
        private Font labelFont;
        private int boardSizeInCells;
        private int cellSize;

        public GraphicsPainter(Graphics graphics, TextBox textBox)
        {
            this.graphics = graphics;
            messageTextBox = textBox;

            //we need to reserve additional cell for a bar with digits - hence "+ 1" in the following line
            boardSizeInCells = GameConfig.BoardSize + 1;
            cellSize = GameConfig.DrawingRectSideSize / boardSizeInCells;

            boardFont = new Font(FontFamily.GenericSansSerif, cellSize / 3);
            labelFont = new Font(FontFamily.GenericSansSerif, GameConfig.PlayerLabelFontSize);
        }

        public void DrawGame(Player player1, Player player2, string message)
        {
            DrawPlayersBoard(player1, GameConfig.Player1DrawingRectLocation);
            DrawPlayersBoard(player2, GameConfig.Player2DrawingRectLocation);
            messageTextBox.Text = message;
        }

        private void DrawPlayersBoard(Player player, Point drawingRectLocation)
        {
            Point cellLocation = new Point(0, 0);
            Brush actualBrush;

            graphics.DrawString(player.Name, labelFont, Brushes.Black, new Point(drawingRectLocation.X, drawingRectLocation.Y - GameConfig.PlayerLabelFontSize - 5));

            for (int row = 0; row < boardSizeInCells; row++)
            {
                for (int col = 0; col < boardSizeInCells; col++)
                {
                    cellLocation.X = drawingRectLocation.X + row * cellSize;
                    cellLocation.Y = drawingRectLocation.Y + col * cellSize;

                    if (row == 0)
                    {
                        if (col == 0)
                            continue;

                        cellLocation.X += cellSize / 4;
                        cellLocation.Y += cellSize / 4;
                        graphics.DrawString(col.ToString(), boardFont, Brushes.Black, cellLocation);
                    }
                    else
                    {
                        if (col == 0)
                        {
                            cellLocation.X += cellSize / 4;
                            cellLocation.Y += cellSize / 4;
                            graphics.DrawString(row.ToString(), boardFont, Brushes.Black, cellLocation);
                        }
                        else
                        {
                            GameBoardCube cube = player.GetGameBoardCubeAt(row - 1, col - 1);
                            switch (cube)
                            {
                                case GameBoardCube.Hit:
                                    actualBrush = GameConfig.HitBrush;
                                    break;
                                case GameBoardCube.Miss:
                                    actualBrush = GameConfig.MissBrush;
                                    break;
                                default:
                                    actualBrush = GameConfig.SeaBrush;
                                    break;
                            }

                            graphics.FillRectangle(actualBrush, new Rectangle(cellLocation.X, cellLocation.Y, cellSize, cellSize));
                            graphics.DrawRectangle(Pens.Black, new Rectangle(cellLocation.X, cellLocation.Y, cellSize, cellSize));
                        }
                    }
                }
            }
        }
    }
}