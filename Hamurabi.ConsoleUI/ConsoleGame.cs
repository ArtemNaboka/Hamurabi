using System;
using Hamurabi.Core;
using Hamurabi.Core.Objects.Models;

namespace Hamurabi.ConsoleUI
{
    // Класс-оболочка для консольной версии игры
    public class ConsoleGame
    {
        private int _acrChange;
        private int _bushelsToFeed;
        private int _acresToPlant;

        private bool _inputEntered;

        private Game _game;

        public bool IsGameOver { get; private set; }

        public void Start()
        {
            _game = new Game();
            _game.Start();
            Console.WriteLine("GAME STARTED!");
            Console.WriteLine(_game.GetInitialReport());
        }


        public void GetInput()
        {
            if (_inputEntered)
            {
                Console.WriteLine("Input already entered");
                return;
            }

            try
            {
                Console.WriteLine("How many acres do you wish to buy or sell? (enter a negative amount to sell bushels)");
                _acrChange = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("How many bushels do you wish to feed your people? (each citizen needs 20 bushels a year)");
                _bushelsToFeed = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("How many acres do you wish to plant with seed? (each acre takes one bushel)");
                _acresToPlant = Convert.ToInt32(Console.ReadLine());
                _inputEntered = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Enter valid input (only numbers)");
            }
        }


        public void MakeTurn()
        {
            if (!_inputEntered)
            {
                Console.WriteLine("At first make input");
                return;
            }

            var playerTurn = new PlayerTurnModel
            {
                AcresToPlant = _acresToPlant,
                AcrChange = _acrChange,
                BushelsToFeed = _bushelsToFeed
            };

            var result = _game.MakeTurn(playerTurn);
            ResetInput();

            if (result.TurnHandleResult == TurnHandleResult.ValidationError)
            {
                Console.WriteLine(result.ValidationErrorMessage);
                return;
            }

            if (result.TurnHandleResult == TurnHandleResult.GameOver)
            {
                IsGameOver = true;
            }

            Console.WriteLine(result.Report);
        }


        public void ResetInput()
        {
            _inputEntered = false;
        }
    }
}
