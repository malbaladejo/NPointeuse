using NPointeuse.Infra.Client;
using NPointeuse.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPointeuse.XF.Views.StandardExpected
{
    internal class StandardExpectedTimesViewModel : BindableBase, INavigationAware
    {
        private readonly IStandardExpectedTimeDataService standardExpectedTimeDataService;

        public StandardExpectedTimesViewModel(IStandardExpectedTimeDataService standardExpectedTimeDataService)
        {
            this.standardExpectedTimeDataService = standardExpectedTimeDataService;
        }

        private IReadOnlyCollection<StandardTimeViewModel> BuildDayViewModels()
        {
            try
            {
                var durations = this.standardExpectedTimeDataService.GetExpectedDurations()
                    .ToDictionary(x => x.Key, x => x.Value);
                var viewModels = new List<StandardTimeViewModel>();
                foreach (var duration in durations)
                    viewModels.Add(new StandardTimeViewModel(duration.Key, durations, this.standardExpectedTimeDataService));

                return viewModels;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public IReadOnlyCollection<StandardTimeViewModel> DayViewModels { get; private set; }       

        public void OnNavigatedFrom(INavigationToken token)
        {
            // nothing to do
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            this.DayViewModels = this.BuildDayViewModels();
        }
    }
}