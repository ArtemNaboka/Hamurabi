using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Validators.Abstract;
using System.Text;

namespace Hamurabi.Core.Objects.Validators
{
    public class TurnValidator : IValidator
    {
        public ValidationResultModel Validate(CityDomain cityDomain, PlayerTurnModel turnModel)
        {
            ValidationResultModel valid = new ValidationResultModel()
            {
                HasErrors = false
            };
            StringBuilder sb = new StringBuilder();
            if (turnModel.BushelsToFeed > cityDomain.BushelsCount)
            {
                sb.Append("У вас нет столько бушелей!");
                valid.HasErrors = true;
            }
           
            if (turnModel.AcrChange < 0 && -turnModel.AcrChange > cityDomain.AcresCount)
            {
                sb.Append("У вас нет столько акров для продажи!");
                valid.HasErrors = true;
            }

            if (turnModel.AcrChange > 0 && cityDomain.BushelsCount / cityDomain.AcrCost < turnModel.AcrChange)
            {
                sb.Append("У вас нет столько бушелей, чтобы купить такое кол-во акров!");
                valid.HasErrors = true;
            }

            if (turnModel.AcresToPlant > cityDomain.AlivePeople * 10)
            {
                sb.Append("У вас нет столько людей, чтобы засеять такое кол-во акров!");
                valid.HasErrors = true;
            }

            if (turnModel.BushelsToFeed < 0)
            {
                sb.Append("Вы не можете вводить отрицательное значение в поле для еды!");
                valid.HasErrors = true;
            }

            if (turnModel.AcresToPlant < 0)
            {
                sb.Append("Вы не можете вводить отрицательное значение в поле для посева акров!");
                valid.HasErrors = true;
            }

            if (valid.HasErrors)
            {
                valid.ErrorMessage = sb.ToString();
            }
            return valid;
        }
    }
}
