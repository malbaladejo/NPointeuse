using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace NPointeuse.XF.Views.EditSpecficTime
{
    internal class EditSpecficTimeViewModel : ValidatableBase, INavigationAware
    {
        private readonly ISpecificExpectedTimeDataService specificExpectedTimeDataService;
        private readonly INavigationService navigationService;
        private SpecificExpectedTime specificExpectedTime;
        private Regex DateRegex = new Regex("[0-9]{2}\\/[0-9]{2}\\/[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}");
        private readonly DelegateCommand saveCommand;

        public EditSpecficTimeViewModel(
            ISpecificExpectedTimeDataService specificExpectedTimeDataService,
            INavigationService navigationService)
        {
            this.saveCommand = new DelegateCommand(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand(this.Delete);
            this.CancelCommand = new DelegateCommand(() => this.navigationService.PopAsync());
            this.navigationService = navigationService;
            this.specificExpectedTimeDataService = specificExpectedTimeDataService;
        }

        public ICommand SaveCommand => this.saveCommand;
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        private string date;
        [DateValidation]
        public string Date
        {
            get => this.date;
            set
            {
                if (!this.Set(ref this.date, value))
                    return;

                this.specificExpectedTime.Date = DateTime.Parse(value);
                this.saveCommand.RaiseCanExecuteChanged();
            }
        }

        private string duration;
        [DurationValidationAttribute]
        public string Duration
        {
            get => this.duration;
            set
            {
                if (!this.Set(ref this.duration, value))
                    return;

                this.specificExpectedTime.Duration = TimeSpan.Parse(duration);
                this.saveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanSave() => !this.HasErrors;

        private void Save()
        {
            if (!this.CanSave())
                return;

            this.specificExpectedTimeDataService.Save(this.specificExpectedTime);
            this.navigationService.PopAsync();
        }

        private void Delete()
        {
            this.specificExpectedTimeDataService.Delete(this.specificExpectedTime);
            this.navigationService.PopAsync();
        }

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            var currentToken = (EditSpecficTimeNavigationToken)token;

            this.specificExpectedTime = currentToken.SpecificExpectedTime.Clone();
            this.date = this.specificExpectedTime.Date.ToString();
            this.duration = this.specificExpectedTime.Duration.ToString();
        }
    }
}