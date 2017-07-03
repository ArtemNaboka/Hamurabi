using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects.Validators.Abstract
{
    public interface IValidator
    {
        ValidationResultModel Validate(CityDomain cityDomain, PlayerTurnModel turnModel);
    }
}