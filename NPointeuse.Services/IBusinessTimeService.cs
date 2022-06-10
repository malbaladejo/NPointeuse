using System;

namespace NPointeuse.Services
{
    public interface IBusinessTimeService
    {
        void Start();
        void Stop();

        bool IsRunning();
        // TimeSpan GetDurations(DateTime beginDate, DateTime endDate);

        TimeSpan GetTodayDuration();

        TimeSpan GetCurrentWeekDuration();

        TimeSpan GetLastTwoMontesDuration();

        TimeSpan GetAllTimesDuration();

       // TimeSpan GetExpectedTime(DateTime beginDate, DateTime endDate);

        TimeSpan GetTodayExpectedTime();

        TimeSpan GetCurrentWeekExpectedTime();

        TimeSpan GetLastTwoMonthesExpectedTime();

        TimeSpan GetAllTimesExpectedTime();
    }
}
