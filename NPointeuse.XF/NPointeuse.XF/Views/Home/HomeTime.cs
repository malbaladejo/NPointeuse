using NPointeuse.Services;
using System;
using Xamarin.Forms;

namespace NPointeuse.XF.Views.Home
{
    internal class HomeTime
    {
        public HomeTime(TimeSpan time, TimeSpan expectedTime)
        {
            this.Time = time.FormatTime();

            var remainingTime = expectedTime - time;
            this.RemainingTimeColor = remainingTime.TotalMinutes >= 0 ? Color.Black : Color.Red;
            this.RemainingTime = remainingTime.FormatTime();
        }

        public string Time { get; }
        public string RemainingTime { get; }

        public Color RemainingTimeColor { get; }
    }
}