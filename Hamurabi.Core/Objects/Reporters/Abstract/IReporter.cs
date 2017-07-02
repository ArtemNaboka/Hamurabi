using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects.Reporters.Abstract
{
    public interface IReporter
    {
        string GenerateYearReport(HandleResult result);
    }
}