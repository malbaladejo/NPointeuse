using System;
using System.Collections.Generic;

namespace NPointeuse.Services.Mock
{
    internal class SpecificExpectedTimeDataServiceMock : ISpecificExpectedTimeDataService
    {
        public IReadOnlyCollection<ISpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate)
        {
            return new ISpecificExpectedTime[0];
        }
    }
}
