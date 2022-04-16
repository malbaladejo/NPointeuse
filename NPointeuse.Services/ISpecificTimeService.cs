using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface ISpecificExpectedTimeDataService
    {
        IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate);

        void Save(SpecificExpectedTime time);
    }
}