using System;

namespace NPointeuse.Services
{
    public static class DateRangeExtensions
    {
        public static bool Overlap(this DateRange date, DateTime startDate, DateTime endDate)
            => date.BeginDate < endDate && startDate < date.CurrentEndDate();

        public static DateTime CurrentEndDate(this DateRange date)
           => date.EndDate.HasValue ? date.EndDate.Value : DateTime.Now;

        public static TimeSpan GetDuration(this DateRange date)
           => date.CurrentEndDate() - date.BeginDate;

        public static bool IsPending(this DateRange date) => date.EndDate == null;
    }
}

