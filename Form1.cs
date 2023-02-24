namespace Battleship
{
    public partial class Form1 : Form
    {
        private Game game;
        private bool gameStarted;

        public Form1()
        {
            InitializeComponent();
            game = new Game(new GraphicsPainter(this.CreateGraphics(), gameStatusTextBox));
            gameStarted = false;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                startBtn.Text = "Show next move...";
                gameStarted = true;
                game.Start();
            }
            else
            {
                if (!game.NextMove())
                {
                    startBtn.Text = "Start game";
                    gameStarted = false;
                }
            }
        }
    }
}