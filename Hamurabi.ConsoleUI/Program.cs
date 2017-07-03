namespace Hamurabi.ConsoleUI
{
    public class Program
    {
        private static void Main()
        {
            var game = new ConsoleGame();
            game.Start();

            while (!game.IsGameOver)
            {
                game.GetInput();
                game.MakeTurn();
            }
        }
    }
}