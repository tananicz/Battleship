using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class GameConfig
    {
        public static readonly int BoardSize = 10;
        public static readonly int[] ShipSizes = { 5, 4, 3, 2, 2, 1, 1 };
        public static readonly int TotalShipsShots = ShipSizes.Sum();
        public static readonly Point Player1DrawingRectLocation = new Point(10, 40);
        public static readonly Point Player2DrawingRectLocation = new Point(335, 40);
        public static readonly int DrawingRectSideSize = 250;
        public static readonly int PlayerLabelFontSize = 15;
        public static readonly Brush HitBrush = Brushes.Red;
        public static readonly Brush MissBrush = Brushes.White;
        public static readonly Brush SeaBrush = Brushes.Navy;
        public static readonly string Player1Name = "Jonny";
        public static readonly string Player2Name = "Sonny";
    }
}
