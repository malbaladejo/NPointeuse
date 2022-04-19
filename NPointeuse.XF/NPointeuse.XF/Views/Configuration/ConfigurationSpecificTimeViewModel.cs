using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.SpecficExpected;
using System;
using System.Linq;
using System.Windows.Input;

namespace NPointeuse.XF.Views.Configuration
{
    internal class ConfigurationSpecificTimeViewModel : BindableBase, IConfigurationSpecificTimeViewModel
    {
        private readonly ISpecificExpectedTimeDataService specificExpectedTimeDataService;
        private readonly INavigationService navigationService;
        private string label;

        public ConfigurationSpecificTimeViewModel(
            ISpecificExpectedTimeDataService specificExpectedTimeDataService,
            INavigationService navigationService)
        {
            this.specificExpectedTimeDataService = specificExpectedTimeDataService;
            this.navigationService = navigationService;
            this.ShowAllCommand = new DelegateCommand(this.ShowAll);
        }

        private void ShowAll()
        {
            this.navigationService.PushAsync(new SpecficExpectedTimesNavigationToken());
        }

        public ICommand ShowAllCommand { get; }

        public string Label
        {
            get => this.label;
            private set => Set(ref this.label, value);
        }

        private void BuildStandardLabel()
        {
            var expectedTimes = this.specificExpectedTimeDataService.GetExpectedDurations(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(14));
            this.Label = string.Empty;
            foreach (var expectedTime in expectedTimes)
            {
                this.Label += $"{expectedTime.Date.ToShortDateString()} : {expectedTime.Duration.FormatTime()}{Environment.NewLine}";
            }

            if (string.IsNullOrEmpty(this.Label))
                this.Label = "No specific time around.";
        }

        public void OnNavigatedTo(INavigationToken token) => this.BuildStandardLabel();

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }
    }
}
