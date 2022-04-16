using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPointeuse.Services;

namespace NPointeuse.Services.Mock
{
    internal class TimeDataServiceMock : ITimeDataService
    {
        private int id = 0;
        private readonly List<DateRange> times = new List<DateRange>();
        private DateTime? pendingTime;

        public TimeDataServiceMock()
        {
            for (int i = 120; i > 0; i--)
            {
                var date = DateTime.Today.AddDays(-i);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    times.Add(new DateRange { Id = id, BeginDate = date.AddHours(8), EndDate = date.AddHours(12) });
                    times.Add(new DateRange { Id = id, BeginDate = date.AddHours(13), EndDate = date.AddHours(17) });
                    id++;
                }
            }

            var today = DateTime.Now.AddHours(-5);
            times.Add(new DateRange { Id = id, BeginDate = today, EndDate = today.AddMinutes(5*60 + 12) });
        }

        public IReadOnlyCollection<DateRange> GetDateRangeForPeriod(DateTime beginDateTime, DateTime endDateTime)
        {
            var realTimes = this.times.Where(t => t.BeginDate >= beginDateTime.BeginOfDay() && t.EndDate <= endDateTime.EndOfDay())
                .ToList();

            if (IsRunning())
                realTimes.Add(new DateRange { BeginDate = this.pendingTime.Value });

            return realTimes;
        }

        public bool IsRunning() => this.pendingTime.HasValue;

        public void Start() => this.pendingTime = DateTime.Now;

        public void Stop()
        {
            this.id++;
            this.times.Add(new DateRange { BeginDate = this.pendingTime.Value, EndDate = DateTime.Now, Id = this.id });
            this.pendingTime = null;
        }

        public DateTime? PendingTime() => this.pendingTime;
    }
    
}
