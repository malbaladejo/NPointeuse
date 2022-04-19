using NPointeuse.Infra.Client;
using NPointeuse.Infra.Client.Collections;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.EditSpecficTime;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimesViewModel : BindableBase
    {
        private readonly ISpecificExpectedTimeDataService specificExpectedTimeDataService;
        private readonly INavigationService navigationService;
        private const int pageSize = 10000;

        public SpecficExpectedTimesViewModel(
            ISpecificExpectedTimeDataService specificExpectedTimeDataService,
            INavigationService navigationService)
        {
            this.AddCommand = new DelegateCommand(this.Add);
            this.specificExpectedTimeDataService = specificExpectedTimeDataService;
            this.navigationService = navigationService;
            this.LoadPage(0);
        }

        private void Add()
        {
            var token = new EditSpecficTimeNavigationToken(new SpecificExpectedTime());
            this.navigationService.PushModalAsync(token);
        }

        private void LoadPage(int page)
        {
            var times = this.specificExpectedTimeDataService.GetExpectedDurations(page, pageSize);
            this.SpecificExpectedTimes.AddRange(times);
        }

        public ICommand AddCommand { get; }

        public ObservableCollection<SpecificExpectedTime> SpecificExpectedTimes { get; } = new ObservableCollection<SpecificExpectedTime>();
    }
}