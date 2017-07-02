﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.TurnHandlers.Abstract;

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


        public void Initialize()
        {
            InitializeFromXml();

            _currentYear = 1;

            _cityDomain = new CityDomain
            {
                CurrentYear = 1
            };
        }


        public HandleResult HandleTurn(PlayerTurnModel model)
        {
            _currentYear = ++_cityDomain.CurrentYear;
            _cityDomain.AcresCount -= model.AcrChange;

            var fedPeopleCount = model.BushelsToFeed / _normFoodForPerson;
            _cityDomain.StarvedPeople = _cityDomain.AlivePeople - fedPeopleCount;
            _cityDomain.ComingInCurrentYearPeople += GameRandom.Next(2, 11);
            _cityDomain.AlivePeople -= _cityDomain.StarvedPeople;
            _cityDomain.AlivePeople += _cityDomain.ComingInCurrentYearPeople;

            _cityDomain.AcrCost = GameRandom.Next(16, 26);

            var eatenByRatsPercent = GameRandom.NextDouble();
            _cityDomain.EatenByRats = _cityDomain.BushelsCount - (int)(_cityDomain.BushelsCount * (eatenByRatsPercent >= 0.6 
                                                                                                    ? eatenByRatsPercent - 0.3
                                                                                                    : eatenByRatsPercent));

            _cityDomain.BushelsCount -= _cityDomain.EatenByRats + model.AcresToPlant + model.BushelsToFeed;
            _cityDomain.BushelsCount += _cityDomain.BushelsCount < 3000
                                        ? GameRandom.Next(1, 6) * model.AcresToPlant
                                        : _cityDomain.AcresCount;



            return new HandleResult
            {
                CityDomain = _cityDomain.Clone(),
                IsGameOver = _currentYear > _maxYears || _cityDomain.AlivePeople <= 0
            };
        }


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void InitializeFromXml()
        {
            var xDoc = XDocument.Load(SettingsPath);
            var xSettings = xDoc.Element("game")?.Element("options");

            _maxYears = Convert.ToInt32(xSettings.Element("years").Value);
            _normFoodForPerson = Convert.ToInt32(xSettings.Element("bushels").Attribute("norm").Value);
        }
    }
}
