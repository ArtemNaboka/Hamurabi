namespace Hamurabi.Core.Objects.Models
{
    public class HandleResult
    {        
        public TurnHandleResult TurnHandleResult { get; set; }
        public GameOverCause GameOverCause { get; set; }
        public CityDomain CityDomain { get; set; }
        public ValidationResultModel ValidationResult { get; set; }
    }
}