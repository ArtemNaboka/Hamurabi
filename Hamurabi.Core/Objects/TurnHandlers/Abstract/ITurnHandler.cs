namespace Hamurabi.Core.Objects.TurnHandlers.Abstract
{
    public interface ITurnHandler
    {
        void Initialize();
        CityDomain HandleTurn(PlayerTurnModel model);
    }
}