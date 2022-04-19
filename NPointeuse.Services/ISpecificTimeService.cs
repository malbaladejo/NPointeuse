using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface ISpecificExpectedTimeDataService
    {
        IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate);

        IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(int page, int pageSize);

        void Save(SpecificExpectedTime time);
    }
}