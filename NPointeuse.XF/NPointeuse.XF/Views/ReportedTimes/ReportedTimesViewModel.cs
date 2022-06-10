using NPointeuse.Infra.Client;
using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.EditReportedTime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NPointeuse.XF.Views.ReportedTimes
{
    internal class ReportedTimesItemViewModel<T>
    {

        public string ReportedTime { get; }

        public string ExpectedTime { get; }

        public string Title { get; }

        public IReadOnlyCollection<T> Items { get; }
    }

    internal class DateRangeReportedTimesItemViewModel
    {
        public DateRangeReportedTimesItemViewModel(DateRange dateRange)
        {
            this.StartTime = dateRange.BeginDate.FormatHoursMinutes();
            this.EndTime = dateRange.EndDate.HasValue ? dateRange.EndDate.Value.FormatHoursMinutes() : "-";
            this.Duration = dateRange.GetDuration().FormatTime();
        }

        public string StartTime { get; }
        public string EndTime { get; }
        public string Duration { get; }
    }

    internal class DayReportedTimesItemViewModel
    {
        public DayReportedTimesItemViewModel(IReadOnlyCollection<DateRange> dateRanges, IBusinessTimeService businessTimeService)
        {
            this.Title = $"From {dateRanges.First().BeginDate.FormatLongDate()} to {dateRanges.Last().BeginDate.FormatLongDate()}";
            var totalReportedSeconds = dateRanges.Sum(d => d.GetDuration().TotalSeconds);
            this.ReportedTime = TimeSpan.FromSeconds(totalReportedSeconds).FormatTime();

        }

        public string ReportedTime { get; }

        public string ExpectedTime { get; }

        public string Title { get; }
    }

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
            this.navigationService.PushAsync(new EditReportedTimeNavigationToken(new DateRange { BeginDate = DateTime.Now }));
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

    internal class ReportedTimesBootstrapper
    {
        private readonly IContainer container;
        private readonly IViewContainer viewContainer;

        public ReportedTimesBootstrapper(IContainer container, IViewContainer viewContainer)
        {
            this.container = container;
            this.viewContainer = viewContainer;
        }

        public void Initialize()
        {
            viewContainer.Register<ReportedTimesNavigationToken, ReportedTimesPage, ReportedTimesViewModel>();
            viewContainer.Register<ReportedTimesNavigationToken2, ReportedTimesPage2, ReportedTimesViewModel2>();
        }
    }
}