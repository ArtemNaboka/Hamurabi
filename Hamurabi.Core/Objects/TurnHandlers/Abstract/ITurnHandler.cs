using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects.TurnHandlers.Abstract
{
    public interface ITurnHandler
    {
        CityDomain InitialDomain { get; }
        void Initialize();
        HandleResult HandleTurn(PlayerTurnModel model);
    }
}