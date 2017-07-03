using System;
using System.Text;
using Hamurabi.Core.Objects.Analysis.Abstract;

namespace Hamurabi.Core.Objects.Analysis
{
    public class DefaultGameOverAnalyst : IGameOverAnalyst
    {
        private readonly string _lineSeparator = Environment.NewLine;

        public string MakeAnalysis(GameOverCause cause, CityDomain intialCityDomain, CityDomain currentCityDomain)
        {
            var sb = new StringBuilder()
                .Append("GAME OVER!")
                .Append(_lineSeparator);

            switch (cause)
            {
                case GameOverCause.PeopleDead:
                    sb.Append(PeopleDeadAnalysis());
                    break;
                case GameOverCause.CameLastYear:
                    sb.Append(LastYearCameAnalysis(intialCityDomain, currentCityDomain));
                    break;
                default:
                    throw new ArgumentException("Need cause to over the game");
            }

            return sb.ToString();
        }


        private string PeopleDeadAnalysis()
        {
            return $"You starved so many people!!!{_lineSeparator}";
        }


        public string LastYearCameAnalysis(CityDomain intialCityDomain, CityDomain currentCityDomain)
        {
            return $"Congratulations!!! You have live to the last year!{_lineSeparator}";
        }
    }
}
