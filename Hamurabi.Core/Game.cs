using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Reporters;
using Hamurabi.Core.Objects.Reporters.Abstract;
using Hamurabi.Core.Objects.TurnHandlers;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;
using Hamurabi.Core.Objects.Validators;

namespace Hamurabi.Core
{
    public class Game
    {
        private readonly ITurnHandler _turnHandler;
        private readonly IReporter _reporter;


        public Game()
        {
            // По-хорошему здесь должен применяться механизм
            // внедрения зависимостей, но так как приложение
            // учебное, то объекты создаются явно
            _turnHandler = new DefaultTurnHandler(new TurnValidator());
            _reporter = new DefaultReporter();
        }


        public void Start()
        {
            _turnHandler.Initialize();
        }


        public TurnResultModel MakeTurn(PlayerTurnModel model)
        {
            var handleResult = _turnHandler.HandleTurn(model);
            var turnResult = new TurnResultModel
            {
                TurnHandleResult = handleResult.TurnHandleResult
            };

            switch (handleResult.TurnHandleResult)
            {
                case TurnHandleResult.ValidationError:
                    turnResult.ValidationErrorMessage = handleResult.ValidationResult.ErrorMessage;
                    break;
                case TurnHandleResult.GameOver:
                case TurnHandleResult.Succeed:
                    turnResult.Report = _reporter.GenerateYearReport(handleResult);
                    break;
            }

            return turnResult;
        }
    }
}
