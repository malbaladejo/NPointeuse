using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.EditReportedTime;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPointeuse.XF.Views.ReportedTimes
{
    internal class ReportedTimesViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly ITimeDataService timeDataService;
        private const int pageSize = 100000;

        public ReportedTimesViewModel(
            INavigationService navigationService,
            ITimeDataService timeDataService)
        {
            this.navigationService = navigationService;
            this.timeDataService = timeDataService;
            this.CancelCommand = new DelegateCommand(() => this.navigationService.PopAsync());
            this.EditCommand = new DelegateCommand<DateRange>(this.Edit);
            this.AddCommand = new DelegateCommand(this.Add);
        }

        private void Edit(DateRange item)
        {
            this.navigationService.PushAsync(new EditReportedTimeNavigationToken(item));
        }

        private void Add()
        {
            this.navigationService.PushAsync(new EditReportedTimeNavigationToken(new DateRange()));
        }

        public ICommand CancelCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand EditCommand { get; }

        public ObservableCollection<DateRange> ReportedTimes { get; } = new ObservableCollection<DateRange>();

        private void LoadPage(int page)
        {
            foreach (var item in this.timeDataService.GetDateRanges(page, pageSize))
            {
                this.ReportedTimes.Add(item);
            }
        }

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            this.LoadPage(0);
        }
    }
}