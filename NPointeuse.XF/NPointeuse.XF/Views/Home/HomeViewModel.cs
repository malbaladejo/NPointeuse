using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.ReportedTimes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NPointeuse.XF.Views.Home
{
    internal class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IBusinessTimeService businessTimeService;
        private readonly INavigationService navigationService;
        private bool isRunning;
        private string buttonLabel;

        public HomeViewModel(IBusinessTimeService businessTimeService, INavigationService navigationService, ITimeDataService timeDataService)
        {
            this.businessTimeService = businessTimeService;
            this.navigationService = navigationService;
            this.ToogleCommand = new DelegateCommand(this.Toggle);
            this.OpenReportedTimesCommand = new DelegateCommand(this.OpenReportedTimes);
            var t = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);


            // DEBUG

            //var times = timeDataService.GetDateRangeForPeriod(DateTime.Today.FirstDayOfWeek(), DateTime.Today.LastDayOfWeek());


            //foreach (var time in times)
            //{
            //    if (time.Id == 637872479465783290)
            //    {
            //        time.BeginDate = new DateTime(2022, 5, 4, 8, 5, 0);
            //        timeDataService.Save(time);
            //    }
            //    if (time.Id == 637872670764888490)
            //    {
            //        time.BeginDate = new DateTime(2022, 5, 4, 13, 20, 0);
            //        timeDataService.Save(time);
            //    }

            //}

            //time.BeginDate = new DateTime(2022, 5, 3, 13, 10, 0);
        }

        private void OpenReportedTimes()
        {
            this.navigationService.PushAsync(new ReportedTimesNavigationToken());
        }

        public ICommand OpenReportedTimesCommand { get; }

        public bool IsRunning
        {
            get => this.isRunning;
            private set
            {
                this.Set(ref this.isRunning, value);
            }
        }

        public string ButtonLabel
        {
            get => this.buttonLabel;
            private set => this.Set(ref this.buttonLabel, value);
        }

        private HomeTime todayTime;
        public HomeTime TodayTime
        {
            get => this.todayTime;
            private set => this.Set(ref this.todayTime, value);

        }

        private HomeTime weekTime;
        public HomeTime WeekTime
        {
            get => this.weekTime;
            private set => this.Set(ref this.weekTime, value);

        }

        private HomeTime twoLastMonthesTime;
        public HomeTime TwoLastMonthesTime
        {
            get => this.twoLastMonthesTime;
            private set => this.Set(ref this.twoLastMonthesTime, value);

        }

        public ICommand ToogleCommand { get; }


        private void Toggle()
        {
            if (this.IsRunning)
                this.businessTimeService.Stop();
            else
                this.businessTimeService.Start();

            this.Refresh();
        }

        private CancellationTokenSource refreshTokenSource;

        private void Refresh()
        {
            this.refreshTokenSource?.Cancel();
            this.refreshTokenSource = new CancellationTokenSource();

            this.RefreshAsync(this.refreshTokenSource.Token);
        }

        private async void RefreshAsync(CancellationToken refreshToken)
        {
            const int oneMinute = 60000;
            while (!refreshToken.IsCancellationRequested)
            {
                this.RefreshIsRunning();
                this.RefreshCurrentTime();
                this.RefreshButtonLabel();

                await Task.Delay(oneMinute);
            }
        }

        private void RefreshButtonLabel()
        {
            this.ButtonLabel = this.isRunning ? "Stop" : "Start";
        }

        private void RefreshIsRunning()
        {
            this.IsRunning = this.businessTimeService.IsRunning();
        }

        private void RefreshCurrentTime()
        {
            //this.CurrentTime = FormatTime(this.businessTimeService.GetTodayDuration());
            this.WriteTimes();
        }

        public void WriteTimes()
        {
            this.TodayTime = new HomeTime(businessTimeService.GetTodayDuration(), businessTimeService.GetTodayExpectedTime());
            this.WeekTime = new HomeTime(businessTimeService.GetCurrentWeekDuration(), businessTimeService.GetCurrentWeekExpectedTime());
            this.TwoLastMonthesTime = new HomeTime(businessTimeService.GetLastTwoMontesDuration(), businessTimeService.GetLastTwoMonthesExpectedTime());
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            this.Refresh();
        }

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }
    }

    internal class HomeTime
    {
        public HomeTime(TimeSpan time, TimeSpan expectedTime)
        {
            Time = time.FormatTime();
            ExpectedTime = (expectedTime - time).FormatTime();
        }

        public string Time { get; }
        public string ExpectedTime { get; }
    }
}