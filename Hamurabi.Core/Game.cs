using Hamurabi.Core.Objects.Reporters.Abstract;
using Hamurabi.Core.Objects.TurnHandlers;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;

namespace Hamurabi.Core
{
    public class Game
    {
        private readonly ITurnHandler _turnHandler;
        private readonly IReporter _reporter;


        public Game()
        {
            _turnHandler = new DefaultTurnHandler();
        }


        public void Start()
        {
            _turnHandler.Initialize();
        }
    }
}
