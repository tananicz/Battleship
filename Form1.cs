namespace Battleship
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Button button1;

        private void button1_Click(object sender, EventArgs e)
        {
            System.Drawing.Graphics g = this.CreateGraphics();
            
            Player player = new Player();
            for (int x = 0; x < GameConfig.BoardSize; x++)
                for (int y = 0; y < GameConfig.BoardSize; y++)
                {
                    Rectangle rect = new Rectangle(new Point(x * 20, y * 20), new Size(20, 20));
                    Brush b = player.IsShip(x, y) ? Brushes.Red : Brushes.Blue;
                    g.FillRectangle(b, rect);
                }
        }
    }
}