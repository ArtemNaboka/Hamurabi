using System;
using Hamurabi.Core;
using Hamurabi.Core.Objects.Models;

namespace Hamurabi.ConsoleUI
{
    public class Program
    {
        private static void Main()
        {
            Game game = new Game();
            game.Start();
            Console.WriteLine("GAME STARTED!");

            TurnResultModel result;
            do
            {
                Console.WriteLine("How many acres do you wish to buy or sell? (enter a negative amount to sell bushels)");
                int arcChange = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("How many bushels do you wish to feed your people? (each citizen needs 20 bushels a year)");
                int bushelsToFeed = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("How many acres do you wish to plant with seed? (each acre takes one bushel)");
                int acresToPlant = Convert.ToInt32(Console.ReadLine());

                var turnModel = new PlayerTurnModel
                {
                    AcrChange = arcChange,
                    BushelsToFeed = bushelsToFeed,
                    AcresToPlant = acresToPlant
                };

                result = game.MakeTurn(turnModel);
                
                Console.WriteLine(result.Report);
            } while (result.TurnHandleResult != TurnHandleResult.GameOver);
        }
    }
}