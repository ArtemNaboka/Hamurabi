﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;
using Hamurabi.Core.Objects.Validators.Abstract;

namespace Hamurabi.Core.Objects.TurnHandlers
{
    public class DefaultTurnHandler : ITurnHandler
    {
        private const string SettingsPath = @"../Hamurabi.Core/GameSettings.xml";
        private static readonly Random GameRandom = new Random();

        private int _normFoodForPerson;
        private int _maxYears;
        private int _currentYear;

        private CityDomain _cityDomain;
        private CityDomain _initialCityDomain;
        private readonly IValidator _validator;


        public DefaultTurnHandler(IValidator validator)
        {
            _validator = validator;
        }


        public void Initialize()
        {
            InitializeFromXml();

            _currentYear = 1;

            _cityDomain = _initialCityDomain.Clone();
        }


        public HandleResult HandleTurn(PlayerTurnModel model)
        {
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

            _cityDomain.ComingInCurrentYearPeople += GameRandom.Next(2, 11);
            _cityDomain.AlivePeople += _cityDomain.ComingInCurrentYearPeople;

            var fedPeopleCount = model.BushelsToFeed / _normFoodForPerson;
            _cityDomain.StarvedPeople = _cityDomain.AlivePeople - fedPeopleCount;          
            _cityDomain.AlivePeople -= _cityDomain.StarvedPeople;
            

            _cityDomain.AcrCost = GameRandom.Next(16, 26);


            _cityDomain.BushelsCount -= model.AcresToPlant + model.BushelsToFeed;
            _cityDomain.HarvestedBushelsPerAcr = _cityDomain.BushelsCount < 3000
                                                ? GameRandom.Next(1, 6)
                                                : 1;

            _cityDomain.BushelsCount += _cityDomain.HarvestedBushelsPerAcr * model.AcresToPlant
                                            - model.AcrChange * _cityDomain.AcrCost;

            var eatenByRatsPercent = GameRandom.NextDouble();

            _cityDomain.EatenByRats = (int)(_cityDomain.BushelsCount * (eatenByRatsPercent >= 0.6
                                                                        ? eatenByRatsPercent - 0.3
                                                                        : eatenByRatsPercent));
            _cityDomain.BushelsCount -= _cityDomain.EatenByRats;


            return new HandleResult
            {
                CityDomain = _cityDomain.Clone(),
                TurnHandleResult = _currentYear > _maxYears || _cityDomain.AlivePeople <= 0
                                    ? TurnHandleResult.GameOver
                                    : TurnHandleResult.Succeed
            };
        }


        public CityDomain InitialDomain => _initialCityDomain.Clone();


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void InitializeFromXml()
        {
            var xDoc = XDocument.Load(SettingsPath);
            var xSettings = xDoc.Element("game")?.Element("options");

            _maxYears = Convert.ToInt32(xSettings.Element("years").Value);
            _normFoodForPerson = Convert.ToInt32(xSettings.Element("bushels").Attribute("norm").Value);


            var xInitial = xDoc.Element("game").Element("initial");
            _initialCityDomain = new CityDomain
            {
                AlivePeople = Convert.ToInt32(xInitial.Attribute("cityPopulation").Value),
                AcrCost = Convert.ToInt32(xInitial.Attribute("acrCost").Value),
                AcresCount = Convert.ToInt32(xInitial.Attribute("acresCount").Value),
                BushelsCount = Convert.ToInt32(xInitial.Attribute("bushelsCount").Value),
                ComingInCurrentYearPeople = Convert.ToInt32(xInitial.Attribute("comingPeople").Value),
                CurrentYear = Convert.ToInt32(xInitial.Attribute("currentYear").Value),
                EatenByRats = Convert.ToInt32(xInitial.Attribute("eatenByRats").Value),
                HarvestedBushelsPerAcr = Convert.ToInt32(xInitial.Attribute("harvestedPerAcr").Value),
                StarvedPeople = Convert.ToInt32(xInitial.Attribute("starvedPeople").Value)
            };
        }
    }
}
