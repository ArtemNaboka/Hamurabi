using System;
using System.Text;
using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Reporters.Abstract;

namespace Hamurabi.Core.Objects.Reporters
{
    public class DefaultReporter : IReporter
    {
        private readonly string _lineSeparator = Environment.NewLine;


        public string GenerateYearReport(HandleResult result)
        {
            var reportSb = new StringBuilder(
                $"I beg to report you about {result.CityDomain.CurrentYear} year:{_lineSeparator}");

            if (result.TurnHandleResult == TurnHandleResult.GameOver)
            {
                reportSb
                    .Append("Game over!!!")
                    .Append(_lineSeparator);

                if (AreAllPeopleStarved(result.CityDomain))
                {
                    reportSb
                        .Append("You starved all your people!!")
                        .Append(_lineSeparator);

                    return reportSb.ToString();
                }
            }
            
            reportSb
                .Append(GetStarvedPeopleReport(result.CityDomain.StarvedPeople))
                .Append(GetPeopleComeToCityReport(result.CityDomain.ComingInCurrentYearPeople))
                .Append(GetCityPopulationReport(result.CityDomain.AlivePeople))
                .Append(GetRatsReport(result.CityDomain.EatenByRats))
                .Append(GetHarvestedBushelsReport(result.CityDomain.HarvestedBushelsPerAcr))
                .Append(GetBushelsInStoreReport(result.CityDomain.BushelsCount))
                .Append(GetLandCostReport(result.CityDomain.AcrCost));
            

            return reportSb.ToString();
        }


        private bool AreAllPeopleStarved(CityDomain cityDomain)
        {
            return cityDomain.AlivePeople <= 0;
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

        #endregion
    }
}
