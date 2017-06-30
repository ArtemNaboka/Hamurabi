using System;
using System.Text;
using Hamurabi.Core.Objects.Reporters.Abstract;

namespace Hamurabi.Core.Objects.Reporters
{
    public class DefaultReporter : IReporter
    {
        private readonly string _lineSeparator = Environment.NewLine;
        private readonly CityDomain _cityDomain;

        public DefaultReporter(CityDomain cityDomain, int year = 1)
        {
            _cityDomain = cityDomain;
            _cityDomain.CurrentYear = year;
        }


        public void SetCurrentYear(int year)
        {
            if (year <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be more than zero.");
            }

            _cityDomain.CurrentYear = year;
        }


        public void IncrementYear()
        {
            _cityDomain.CurrentYear++;
        }


        public string GenerateYearReport()
        {
            var reportSb = new StringBuilder($"I beg to report you about {_cityDomain.CurrentYear} year:{_lineSeparator}");


            return reportSb.ToString();
        }


        #region Separate reports
        
        private string GetStarvedPeopleReport(int starvedCount)
        {
            return $"{starvedCount} people starved";
        }


        private string GetPeopleComeToCityReport(int comingPeopleCount)
        {
            return $"{comingPeopleCount} people came to the city";
        }


        private string GetCityPopulationReport(int population)
        {
            return $"The city population is now {population}";
        }


        private string GetRatsReport(int eatenBushelsCount)
        {
            return $"Rats ate {eatenBushelsCount} bushels";
        }


        private string GetHarvestedBushelsReport(int bushelsPerAcr)
        {
            return $"You harvested {bushelsPerAcr} bushels per acre";
        }


        private string GetBushelsInStoreReport(int bushelsCount)
        {
            return $"You now have {bushelsCount} bushels in store";
        }


        private string GetLandCostReport(int bushelsPerAcr)
        {
            return $"Land is trading at {bushelsPerAcr} bushels per acre";
        }

        #endregion
    }
}
