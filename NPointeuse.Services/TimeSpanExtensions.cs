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
            return $"{minus}{FormatMinutes((int)(totalMinutesAbs / 60))}:{FormatMinutes((int)(totalMinutesAbs % 60))}";
        }

        private static string FormatMinutes(int minutes)
               => minutes.ToString().PadLeft(2, '0');
    }
}
