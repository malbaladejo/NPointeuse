using System;
using System.Collections.Generic;
using System.Linq;

namespace NPointeuse.Services
{
    internal class BusinessTimeService : IBusinessTimeService
    {
        private readonly ITimeDataService timeDataService;
        private readonly IStandardExpectedTimeDataService standardService;
        private readonly ISpecificExpectedTimeDataService specificService;

        public BusinessTimeService(
            ITimeDataService timeDataService,
            IStandardExpectedTimeDataService standardService,
            ISpecificExpectedTimeDataService specificService)
        {
            this.timeDataService = timeDataService;
            this.standardService = standardService;
            this.specificService = specificService;
        }

        public TimeSpan GetTodayDuration() => this.GetDurations(DateTime.Today, DateTime.Today.EndOfDay());

        public TimeSpan GetCurrentWeekDuration() => this.GetDurations(FirstDayOfWeek, LastDayOfWeek);

        public TimeSpan GetLastTwoMontesDuration() => this.GetDurations(DateTime.Today.AddMonths(-2), DateTime.Today.EndOfDay());

        public TimeSpan GetTodayExpectedTime() => this.GetExpectedTime(DateTime.Today, DateTime.Today.EndOfDay());

        public TimeSpan GetCurrentWeekExpectedTime() => this.GetExpectedTime(FirstDayOfWeek, LastDayOfWeek);

        public TimeSpan GetLastTwoMonthesExpectedTime() => this.GetExpectedTime(DateTime.Today.AddMonths(-2), LastDayOfWeek);

        public bool IsRunning() => this.timeDataService.PendingTime().HasValue;

        public void Start() => this.timeDataService.Start();

        public void Stop() => this.timeDataService.Stop();

        private static DateTime FirstDayOfWeek => DateTime.Today.FirstDayOfWeek().BeginOfDay();
        private static DateTime LastDayOfWeek => DateTime.Today.LastDayOfWeek().EndOfDay();

        private TimeSpan GetDurations(DateTime beginDate, DateTime endDate)
        {
            var ticks = this.timeDataService.GetDateRangeForPeriod(beginDate, endDate)
                                        .Sum(d => d.GetDuration().Ticks);

            return TimeSpan.FromTicks(ticks);
        }

        private TimeSpan GetExpectedTime(DateTime beginDate, DateTime endDate)
        {
            var specificDurations = this.specificService.GetExpectedDurations(beginDate.BeginOfDay(), endDate.EndOfDay());
            var standardDurations = this.standardService.GetExpectedDurations();
            var date = beginDate.BeginOfDay();

            var durations = new List<TimeSpan>();

            while (date < endDate.EndOfDay())
            {
                var specificDuration = specificDurations.FirstOrDefault(d => d.Date == date);
                if (specificDuration != null)
                {
                    durations.Add(specificDuration.Duration);
                }
                else
                {
                    durations.Add(standardDurations[date.DayOfWeek]);
                }
                date = date.AddDays(1);
            }

            return TimeSpan.FromTicks(durations.Sum(d => d.Ticks));
        }
    }
}
