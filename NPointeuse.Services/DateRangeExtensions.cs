using System;

namespace NPointeuse.Services
{
    public static class DateRangeExtensions
    {
        public static bool Overlap(this DateRange date, DateTime startDate, DateTime endDate)
            => date.BeginDate < endDate && startDate < date.EndDate;

        public static bool IsPending(this DateRange date) => date.EndDate == null;
    }
}
