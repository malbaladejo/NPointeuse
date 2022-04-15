using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface IStandardExpectedTimeDataService 
    {
        IReadOnlyDictionary<DayOfWeek, TimeSpan> GetExpectedDurations();
    }

}
