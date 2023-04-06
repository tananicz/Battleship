namespace Battleship
{
    internal class Game
    {
        private Player player1;
        private Player player2;
        private Player actualPlayer;
        private Player opponent;
        private Random random = new Random();
        private GraphicsPainter gfxPainter;

        public Game(GraphicsPainter gfxPainter)
        {
            this.gfxPainter = gfxPainter;
        }

        public void Start()
        {
            player1 = new Player(GameConfig.Player1Name);
            player2 = new Player(GameConfig.Player2Name);

            actualPlayer = player1;
            opponent = player2;

            if (random.Next(2) == 0)
                SwitchActualPlayers();

            NextMove();
        }

        public bool NextMove()
        {
            if (null != actualPlayer && null != opponent)
            {
                String report = actualPlayer.ShootAt(opponent);

                if (actualPlayer.IsWinner())
                {
                    report += (Environment.NewLine + $"And it happens that {actualPlayer.Name} wins! GAME OVER!");
                    gfxPainter.DrawGame(player1, player2, report);
                    return false;
                }
                else
                {
                    gfxPainter.DrawGame(player1, player2, report);
                    SwitchActualPlayers();
                    return true;
                }
            }
            else
                return false;
        }

        private void SwitchActualPlayers()
        {
            Player tmpPlayer = actualPlayer;
            actualPlayer = opponent;
            opponent = tmpPlayer;
        }
    }
}
