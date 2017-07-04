using Hamurabi.Core.Objects;
using Hamurabi.Core.Objects.Analysis;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Reporters;
using Hamurabi.Core.Objects.Reporters.Abstract;
using Hamurabi.Core.Objects.TurnHandlers;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;
using Hamurabi.Core.Objects.Validators;

namespace Hamurabi.Core
{
    // Класс, предоставляющий API для игры
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
            _reporter = new DefaultReporter(new DefaultGameOverAnalyst());
        }


        public void Start()
        {
            _turnHandler.Initialize();
        }


        public string GetInitialReport()
        {
            return _reporter.GetInitialDomainInfo();
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


        public SettingsModel GetGameSettings()
        {
            return XmlGameInitializer.GetGameSettings();
        }
    }
}
