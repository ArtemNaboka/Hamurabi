using System;
using System.Text;
using Hamurabi.Core.Objects.Analysis.Abstract;

namespace Hamurabi.Core.Objects.Analysis
{
    // Аналитика причины конца игры и генерации соответствующего вывода
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
                    sb.Append(PeopleDeadAnalysis(currentCityDomain));
                    break;
                case GameOverCause.CameLastYear:
                    sb.Append(LastYearCameAnalysis(intialCityDomain, currentCityDomain));
                    break;
                default:
                    throw new ArgumentException("Need cause to over the game");
            }

            return sb.ToString();
        }


        private string PeopleDeadAnalysis(CityDomain currentCityDomain)
        {
            var sb = new StringBuilder()
                .Append($"You starved so many people!!!{_lineSeparator}")
                .Append(currentCityDomain.CurrentYear >= 7
                    ? "You are still an inexperienced ruler"
                    : "You are a bloodthirsty tyrant!")
                .Append(_lineSeparator);

            return sb.ToString();
        }


        public string LastYearCameAnalysis(CityDomain intialCityDomain, CityDomain currentCityDomain)
        {
            var sb = new StringBuilder($"Congratulations!!! You have live to the last year!{_lineSeparator}");

            int compareResult = currentCityDomain.CompareTo(intialCityDomain);
            switch (compareResult)
            {
                case 1:
                    sb.Append("You could exaggerate the wealth of your city!!! You are a great ruler!");
                    break;
                case -1:
                    sb.Append("But you failed to maintain the stability of the city(");
                    break;
                default:
                    sb.Append("Stability is a sign of skill! The state of the city did not change in any way");
                    break;
            }
            sb.Append(_lineSeparator);

            return sb.ToString();
        }
    }
}
