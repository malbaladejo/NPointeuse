using NPointeuse.Infra.Client;
using NPointeuse.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPointeuse.XF.Views.StandardExpected
{
    internal class StandardTimeViewModel : ValidatableBase
    {
        private readonly Dictionary<DayOfWeek, TimeSpan> durations;
        private readonly IStandardExpectedTimeDataService standardExpectedTimeDataService;
        private string duration;

        public StandardTimeViewModel(
            DayOfWeek dayOfWeek,
            Dictionary<DayOfWeek, TimeSpan> durations,
            IStandardExpectedTimeDataService standardExpectedTimeDataService)
        {
            this.DayOfWeek = dayOfWeek;
            this.durations = durations;
            this.standardExpectedTimeDataService = standardExpectedTimeDataService;
            this.duration = this.durations[this.DayOfWeek].ToString();
        }

        public DayOfWeek DayOfWeek { get; }

        [DurationValidationAttribute]
        public string Duration
        {
            get => duration;
            set
            {
                if (!this.Set(ref this.duration, value))
                    return;

                this.SaveDurations();
            }
        }

        private void SaveDurations()
        {
            try
            {
                var newDuration = TimeSpan.Parse(this.duration);

                if (newDuration == this.durations[this.DayOfWeek]) return;

                this.durations[this.DayOfWeek] = TimeSpan.Parse(duration);
                this.standardExpectedTimeDataService.Save(this.durations);
            }
            catch(Exception e)
            {
               
            }
        }
    }
}