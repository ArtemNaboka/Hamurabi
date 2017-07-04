using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;
using Hamurabi.Core.Objects.Validators.Abstract;

namespace Hamurabi.Core.Objects.TurnHandlers
{
    // Обработчик хода пользователя
    public class DefaultTurnHandler : ITurnHandler
    {
        private static readonly Random GameRandom = new Random();

        private const int OneBushelPerAcr = 1;
        private const double ReductionRatsFactor = 0.4;

        private int _normFoodForPerson;
        private int _maxYears;
        private int _currentYear;

        private CityDomain _cityDomain;
        private readonly IValidator _validator;


        public DefaultTurnHandler(IValidator validator)
        {
            _validator = validator;
        }


        public void Initialize()
        {
            InitializeFromXml();

            _currentYear = 1;
        }


        public HandleResult HandleTurn(PlayerTurnModel model)
        {
            _cityDomain.BushelsCount -= model.AcrChange * _cityDomain.AcrCost;
            var validationResult = _validator.Validate(_cityDomain, model);
            if (validationResult.HasErrors)
            {
                return new HandleResult
                {
                    TurnHandleResult = TurnHandleResult.ValidationError,
                    ValidationResult = validationResult
                };
            }


            _currentYear = ++_cityDomain.CurrentYear;
            _cityDomain.AcresCount += model.AcrChange;

            var bushelsForPerson = model.BushelsToFeed / _normFoodForPerson;
            var fedPeopleCount = bushelsForPerson > _cityDomain.AlivePeople
                                                ? _cityDomain.AlivePeople
                                                : bushelsForPerson;

            _cityDomain.StarvedPeople = _cityDomain.AlivePeople - fedPeopleCount;
            _cityDomain.AlivePeople -= _cityDomain.StarvedPeople;

            if (_cityDomain.AlivePeople <= _cityDomain.StarvedPeople)
            {
                return new HandleResult
                {
                    TurnHandleResult = TurnHandleResult.GameOver,
                    CityDomain = _cityDomain.Clone(),
                    GameOverCause = GameOverCause.PeopleDead
                };
            }

            _cityDomain.ComingInCurrentYearPeople = GameRandom.Next(2, 11);
            _cityDomain.AlivePeople += _cityDomain.ComingInCurrentYearPeople;


            _cityDomain.AcrCost = GameRandom.Next(16, 26);


            _cityDomain.BushelsCount -= model.AcresToPlant + model.BushelsToFeed;
            _cityDomain.HarvestedBushelsPerAcr = _cityDomain.BushelsCount < 3000
                                                ? GameRandom.Next(1, 6)
                                                : OneBushelPerAcr;

            _cityDomain.BushelsCount += _cityDomain.HarvestedBushelsPerAcr * model.AcresToPlant;

            var eatenByRatsPercent = GameRandom.NextDouble();

            _cityDomain.EatenByRats = (int)(_cityDomain.BushelsCount * (eatenByRatsPercent >= 0.6
                                                                        ? eatenByRatsPercent - ReductionRatsFactor
                                                                        : eatenByRatsPercent));
            _cityDomain.BushelsCount -= _cityDomain.EatenByRats;


            var result = new HandleResult
            {
                CityDomain = _cityDomain.Clone(),
                TurnHandleResult = _currentYear > _maxYears
                                    ? TurnHandleResult.GameOver
                                    : TurnHandleResult.Succeed,
            };

            result.GameOverCause = result.TurnHandleResult == TurnHandleResult.GameOver
                                    ? GameOverCause.CameLastYear
                                    : GameOverCause.None;
            return result;
        }


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void InitializeFromXml()
        {
            var xDoc = XDocument.Load(StringConstants.SettingsPath);
            var xSettings = xDoc.Element("game")?.Element("options");

            _maxYears = Convert.ToInt32(xSettings.Element("years").Value);
            _normFoodForPerson = Convert.ToInt32(xSettings.Element("bushels").Attribute("norm").Value);

            _cityDomain = XmlGameInitializer.GetInitialCityDomain();
        }
    }
}
