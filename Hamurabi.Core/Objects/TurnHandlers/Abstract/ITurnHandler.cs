using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects.TurnHandlers.Abstract
{
    public interface ITurnHandler
    {
        void Initialize();
        HandleResult HandleTurn(PlayerTurnModel model);
    }
}