using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects
{
    // Работа с настройками игры из xml
    public static class XmlGameInitializer
    {
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static CityDomain GetInitialCityDomain()
        {
            var xDoc = XDocument.Load(StringConstants.SettingsPath);
            var xInitial = xDoc.Element("game").Element("initial");

            var initialCityDomain = new CityDomain
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

            return initialCityDomain;
        }


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static SettingsModel GetGameSettings()
        {
            var xDoc = XDocument.Load(StringConstants.SettingsPath);
            var xSettings = xDoc.Element("game").Element("options").Element("bushels");

            return new SettingsModel
            {
                BushelsForPerson = Convert.ToInt32(xSettings.Attribute("norm").Value),
                BushelsToPlantAcr = Convert.ToInt32(xSettings.Attribute("toPlantAcr").Value)
            };
        }
    }
}
