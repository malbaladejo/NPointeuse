using System;

namespace NPointeuse.Services
{
    public interface IBusinessTimeService
    {
        void Start();
        void Stop();

        bool IsRunning();

        TimeSpan GetTodayDuration();

        TimeSpan GetTodayExpectedTime();

        TimeSpan GetCurrentWeekDuration();

        TimeSpan GetCurrentWeekExpectedTime();

        TimeSpan GetLastTwoMontesDuration();

        TimeSpan GetLastTwoMonthesExpectedTime();
    }
}
