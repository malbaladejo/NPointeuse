using System;

namespace NPointeuse.Services
{
    public static class TimeSpanExtensions
    {
        public static string FormatTime(this TimeSpan time)
        {
            var totalMinutes = time.TotalMinutes;
            var totalMinutesAbs = Math.Abs(totalMinutes);
            var minus = totalMinutes < 0 ? "-" : string.Empty;
            return $"{minus}{((int)(totalMinutesAbs / 60)).FormatNumber()}:{((int)(totalMinutesAbs % 60)).FormatNumber()}";
        }
    }
}
