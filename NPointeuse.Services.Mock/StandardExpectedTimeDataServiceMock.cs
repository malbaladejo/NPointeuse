using System;
using System.Collections.Generic;

namespace NPointeuse.Services.Mock
{
    internal class StandardExpectedTimeDataServiceMock : IStandardExpectedTimeDataService
    {
        public IReadOnlyDictionary<DayOfWeek, TimeSpan> GetExpectedDurations()
        {
            return new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
            };
        }
    }

}
