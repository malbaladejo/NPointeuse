using NPointeuse.Infra.Client;
using NPointeuse.Infra.Client.Collections;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.EditSpecficTime;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimesViewModel : BindableBase
    {
        private readonly ISpecificExpectedTimeDataService specificExpectedTimeDataService;
        private readonly ISpecficExpectedTimeViewModelFactory specficExpectedTimeViewModelFactory;
        private readonly INavigationService navigationService;
        private const int pageSize = 10000;

        public SpecficExpectedTimesViewModel(
            ISpecificExpectedTimeDataService specificExpectedTimeDataService,
            ISpecficExpectedTimeViewModelFactory specficExpectedTimeViewModelFactory,
            INavigationService navigationService)
        {
            this.CancelCommand = new DelegateCommand(() => this.navigationService.PopAsync());
            this.AddCommand = new DelegateCommand(this.Add); 
            
            this.specificExpectedTimeDataService = specificExpectedTimeDataService;
            this.specficExpectedTimeViewModelFactory = specficExpectedTimeViewModelFactory;
            this.navigationService = navigationService;
            this.LoadPage(0);
        }

        private void Add()
        {
            var token = new EditSpecficTimeNavigationToken(new SpecificExpectedTime());
            this.navigationService.PushAsync(token);
        }

        private void LoadPage(int page)
        {
            try
            {
                var times = this.specificExpectedTimeDataService.GetExpectedDurations(page, pageSize);
                this.SpecificExpectedTimes.AddRange(times.Select(i=> this.specficExpectedTimeViewModelFactory.CreateInstance(i)));
            }
            catch(Exception e)
            {

            }
        }

        public ICommand CancelCommand { get; }

        public ICommand AddCommand { get; }

        public ObservableCollection<ISpecficExpectedTimeViewModel> SpecificExpectedTimes { get; } 
            = new ObservableCollection<ISpecficExpectedTimeViewModel>();
    }
}