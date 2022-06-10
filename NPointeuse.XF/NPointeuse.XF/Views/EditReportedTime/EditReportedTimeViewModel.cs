using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace NPointeuse.XF.Views.EditReportedTime
{
    internal class EditReportedTimeViewModel : ValidatableBase, INavigationAware
    {
        private readonly ITimeDataService timeDataService;
        private readonly INavigationService navigationService;
        private DateRange dateRange;
        private Regex DateRegex = new Regex("[0-9]{2}\\/[0-9]{2}\\/[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}");
        private readonly DelegateCommand saveCommand;

        public EditReportedTimeViewModel(ITimeDataService timeDataService, INavigationService navigationService)
        {
            this.saveCommand = new DelegateCommand(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand(this.Delete);
            this.CancelCommand = new DelegateCommand(() => this.navigationService.PopAsync());
            this.timeDataService = timeDataService;
            this.navigationService = navigationService;
        }

        private bool CanSave() => !this.HasErrors;

        private void Save()
        {
            if (!this.CanSave())
                return;

            this.timeDataService.Save(this.dateRange);
            this.navigationService.PopAsync();
        }

        private void Delete()
        {
            this.timeDataService.Delete(this.dateRange);
            this.navigationService.PopAsync();
        }

        public ICommand SaveCommand => this.saveCommand;
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        private string beginDate;

        [MandatoryString]
        [DateValidationAttribute]
        [DateMaxValidation(nameof(EndDate))]
        public string BeginDate
        {
            get => this.beginDate;
            set
            {
                if (!this.Set(ref this.beginDate, value))
                    return;

                this.dateRange.BeginDate = DateTime.Parse(value);
                this.saveCommand.RaiseCanExecuteChanged();
            }
        }

        private string endDate;

        [DateValidationAttribute]
        [DateMinValidation(nameof(BeginDate))]
        public string EndDate
        {
            get => this.endDate;
            set
            {
                if (!this.Set(ref this.endDate, value))
                    return;

                this.dateRange.EndDate = DateTime.Parse(value);
                this.saveCommand.RaiseCanExecuteChanged();
            }
        }

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            var currentToken = (EditReportedTimeNavigationToken)token;

            this.dateRange = currentToken.DateRange.Clone();
            this.beginDate = currentToken.DateRange.BeginDate.ToString();
            this.endDate = currentToken.DateRange.EndDate.ToString();
        }
    }
}