using Hamurabi.Core.Objects.Models;
using Hamurabi.Core.Objects.Validators.Abstract;
using System;
using System.Text;

namespace Hamurabi.Core.Objects.Validators
{
    public class TurnValidator : IValidator
    {
        private readonly string _separator = Environment.NewLine;
        public ValidationResultModel Validate(CityDomain cityDomain, PlayerTurnModel turnModel)
        {
            ValidationResultModel valid = new ValidationResultModel()
            {
                HasErrors = false
            };
            StringBuilder sb = new StringBuilder();
            
            if (turnModel.BushelsToFeed > cityDomain.BushelsCount)
            {
                sb.Append("You do not have as many bushels").Append(_separator);
                valid.HasErrors = true;
            }
           
            if (turnModel.AcrChange < 0 && -turnModel.AcrChange > cityDomain.AcresCount)
            {
                sb.Append("You do not have as many acres for sale!").Append(_separator);
                valid.HasErrors = true;
            }

            if (turnModel.AcrChange > 0 && cityDomain.BushelsCount / cityDomain.AcrCost < turnModel.AcrChange)
            {
                sb.Append("You do not have as many bushels to buy as many acres!").Append(_separator);
                valid.HasErrors = true;
            }

            if (turnModel.AcresToPlant > cityDomain.AlivePeople * 10)
            {
                sb.Append("You do not have so many people to sow such a count of acres!").Append(_separator);
                valid.HasErrors = true;
            }

            if (turnModel.BushelsToFeed < 0)
            {
                sb.Append("You can not enter a negative value in the field for food!").Append(_separator);
                valid.HasErrors = true;
            }

            if (turnModel.AcresToPlant < 0)
            {
                sb.Append("You can not enter a negative value in the field for sowing acres!").Append(_separator);
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
