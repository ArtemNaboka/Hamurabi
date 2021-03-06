﻿using Hamurabi.Core.Objects.Models;

namespace Hamurabi.Core.Objects.Reporters.Abstract
{
    public interface IReporter
    {
        string GetInitialDomainInfo();
        string GetDomainInfo(CityDomain domain);
        string GenerateYearReport(HandleResult result);
    }
}