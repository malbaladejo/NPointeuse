using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.StandardExpected;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace NPointeuse.XF.Views.Configuration
{
    internal class ConfigurationStandardTimeViewModel : BindableBase, IConfigurationStandardTimeViewModel
    {
        private readonly IStandardExpectedTimeDataService standardExpectedTimeDataService;
        private readonly INavigationService navigationService;
        private string label;

        public ConfigurationStandardTimeViewModel(
            IStandardExpectedTimeDataService standardExpectedTimeDataService,
            INavigationService navigationService)
        {
            this.standardExpectedTimeDataService = standardExpectedTimeDataService;
            this.navigationService = navigationService;
            this.EditCommand = new DelegateCommand(this.Edit);
        }

        private void Edit()
        {
            this.navigationService.PushAsync(new StandardExpectedTimesNavigationToken());
        }

        public ICommand EditCommand { get; }

        public string Label
        {
            get => this.label;
            private set => Set(ref this.label, value);
        }

        private void BuildStandardLabel()
        {
            var expectedTimes = this.standardExpectedTimeDataService.GetExpectedDurations();
            this.Label = string.Empty;
            foreach (var expectedTime in expectedTimes.Where(t => t.Value.TotalSeconds > 0))
            {
                this.Label += $"{expectedTime.Key} : {expectedTime.Value.FormatTime()}{Environment.NewLine}";
            }
        }

        public void OnNavigatedTo(INavigationToken token) => this.BuildStandardLabel();

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }
    }
}
