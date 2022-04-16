using System;
using System.Collections.Generic;

namespace NPointeuse.Services.Mock
{
    internal class StandardExpectedTimeDataServiceMock : IStandardExpectedTimeDataService
    {
        private IReadOnlyDictionary<DayOfWeek, TimeSpan> durations = new Dictionary<DayOfWeek, TimeSpan>
        {
            [DayOfWeek.Monday] = TimeSpan.FromHours(8),
            [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
            [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
            [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
            [DayOfWeek.Friday] = TimeSpan.FromHours(8),
            [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
            [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
        };

        public IReadOnlyDictionary<DayOfWeek, TimeSpan> GetExpectedDurations()
        {
            return this.durations;
        }

        public void Save(IReadOnlyDictionary<DayOfWeek, TimeSpan> durations)
        {
            this.durations = durations;
        }
    }
}
