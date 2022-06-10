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

        public HomeViewModel(IBusinessTimeService businessTimeService, INavigationService navigationService)
        {
            this.businessTimeService = businessTimeService;
            this.navigationService = navigationService;
            this.ToogleCommand = new DelegateCommand(this.Toggle);
            this.OpenReportedTimesCommand = new DelegateCommand(this.OpenReportedTimes);
            this.OpenReportedTimesCommand2 = new DelegateCommand(this.OpenReportedTimes2);
            var t = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        private void OpenReportedTimes()
        {
            this.navigationService.PushAsync(new ReportedTimesNavigationToken());
        }

        private void OpenReportedTimes2()
        {
            this.navigationService.PushAsync(new ReportedTimesNavigationToken2());
        }

        public ICommand OpenReportedTimesCommand { get; }

        public ICommand OpenReportedTimesCommand2 { get; }

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
            this.TwoLastMonthesTime = new HomeTime(businessTimeService.GetAllTimesDuration(), businessTimeService.GetAllTimesExpectedTime());
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
}