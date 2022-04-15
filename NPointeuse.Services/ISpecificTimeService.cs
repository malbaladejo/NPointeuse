using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface ISpecificExpectedTimeDataService
    {
        IReadOnlyCollection<ISpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate);
    }
}