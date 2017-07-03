using System;
using System.Text;
using Hamurabi.Core.Objects.Analysis.Abstract;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Reporters.Abstract;

namespace Hamurabi.Core.Objects.Reporters
{
    // Класс, отвечающий за вывод состояния города
    public class DefaultReporter : IReporter
    {
        private readonly string _lineSeparator = Environment.NewLine;
        private readonly IGameOverAnalyst _gameOverAnalyst;

        private CityDomain _initialCityDomain;


        public DefaultReporter(IGameOverAnalyst gameOverAnalyst)
        {
            _gameOverAnalyst = gameOverAnalyst;
        }


        public string GenerateYearReport(HandleResult result)
        {
            var reportSb = new StringBuilder();                          

            if (result.TurnHandleResult == TurnHandleResult.GameOver)
            {
                reportSb
                    .Append(_gameOverAnalyst.MakeAnalysis(result.GameOverCause, _initialCityDomain, result.CityDomain));

                return reportSb.ToString();
            }

            reportSb.Append(GetDomainInfo(result.CityDomain));
            

            return reportSb.ToString();
        }


        public string GetDomainInfo(CityDomain domain)
        {
            var domainSb = new StringBuilder();

            domainSb
                .Append("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~")
                .Append(_lineSeparator)
                .Append($"I beg to report you about {domain.CurrentYear} year:{_lineSeparator}")
                .Append(GetStarvedPeopleReport(domain.StarvedPeople))
                .Append(GetPeopleComeToCityReport(domain.ComingInCurrentYearPeople))             
                .Append(GetCityPopulationReport(domain.AlivePeople))
                .Append(GetAcrsOwningReport(domain.AcresCount))
                .Append(GetRatsReport(domain.EatenByRats))
                .Append(GetHarvestedBushelsReport(domain.HarvestedBushelsPerAcr))
                .Append(GetBushelsInStoreReport(domain.BushelsCount))
                .Append(GetLandCostReport(domain.AcrCost))
                .Append("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~")
                .Append(_lineSeparator);

            return domainSb.ToString();
        }


        public string GetInitialDomainInfo()
        {
            if (_initialCityDomain == null)
            {
                _initialCityDomain = XmlGameInitializer.GetInitialCityDomain();
            }
            return GetDomainInfo(_initialCityDomain);
        }


        #region Separate reports

        private string GetStarvedPeopleReport(int starvedCount)
        {
            return $"{starvedCount} people starved{_lineSeparator}";
        }


        private string GetPeopleComeToCityReport(int comingPeopleCount)
        {
            return $"{comingPeopleCount} people came to the city{_lineSeparator}";
        }


        private string GetCityPopulationReport(int population)
        {
            return $"The city population is now {population}{_lineSeparator}";
        }


        private string GetRatsReport(int eatenBushelsCount)
        {
            return $"Rats ate {eatenBushelsCount} bushels{_lineSeparator}";
        }


        private string GetHarvestedBushelsReport(int bushelsPerAcr)
        {
            return $"You harvested {bushelsPerAcr} bushels per acre{_lineSeparator}";
        }


        private string GetBushelsInStoreReport(int bushelsCount)
        {
            return $"You now have {bushelsCount} bushels in store{_lineSeparator}";
        }


        private string GetLandCostReport(int bushelsPerAcr)
        {
            return $"Land is trading at {bushelsPerAcr} bushels per acre{_lineSeparator}";
        }


        private string GetAcrsOwningReport(int acrsCount)
        {
            return $"The city now owns {acrsCount} acres{_lineSeparator}";
        }

        #endregion
    }
}
