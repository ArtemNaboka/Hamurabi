namespace Hamurabi.Core.Objects.Analysis.Abstract
{
    public interface IGameOverAnalyst
    {
        string MakeAnalysis(GameOverCause cause, CityDomain intialCityDomain, CityDomain currentCityDomain);
    }
}