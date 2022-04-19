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

        public EditReportedTimeViewModel(ITimeDataService timeDataService, INavigationService navigationService)
        {
            this.SaveCommand = new DelegateCommand(this.Save, this.CanSave);
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

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public string BeginDate
        {
            get => this.dateRange.BeginDate.ToString();
            set
            {
                if (!this.IsBeginDateValid(value, out var date))
                    return;

                this.dateRange.BeginDate = date;
                this.RaiseErrorsChanged();
            }
        }

        private bool IsBeginDateValid(string value, out DateTime date)
        {
            this.ClearValidations(nameof(this.BeginDate));

            date = DateTime.Now;

            if (string.IsNullOrEmpty(value))
            {
                this.AddValidation("Begin date can not be null.", ValidationSeverity.Error, nameof(this.BeginDate));
                return false;
            }

            if (!this.DateRegex.IsMatch(value))
            {
                this.AddValidation("Incorrect format. Must be dd/mm/yyyy hh:mm", ValidationSeverity.Error, nameof(this.BeginDate));
                return false;
            }
            
            if (!DateTime.TryParse(value, out date))
            {
                this.AddValidation("Incorrect format. Must be dd/mm/yyyy hh:mm", ValidationSeverity.Error, nameof(this.BeginDate));
                return false;
            }

            if (this.dateRange.EndDate.HasValue && this.dateRange.EndDate.Value < date)
            {
                this.AddValidation("Begin date must be lower than end date", ValidationSeverity.Error, nameof(this.BeginDate));
                return false;
            }

            return true;
        }

        public string EndDate
        {
            get => this.dateRange.EndDate?.ToString();
            set
            {
                if (!this.IsEndDateValid(value, out var date))
                    return;

                this.dateRange.EndDate = date;
                this.RaisePropertyChanged();
            }
        }

        private bool IsEndDateValid(string value, out DateTime? date)
        {
            this.ClearValidations(nameof(this.EndDate));
            date = null;
            
            if (string.IsNullOrEmpty(value))
                return true;

            if (!this.DateRegex.IsMatch(value))
            {
                this.AddValidation("Incorrect format. Must be dd/mm/yyyy hh:mm", ValidationSeverity.Error, nameof(this.EndDate));
                return false;
            }
            
            if (!DateTime.TryParse(value, out var localDate))
            {
                this.AddValidation("Incorrect format. Must be dd/mm/yyyy hh:mm", ValidationSeverity.Error, nameof(this.EndDate));
                return false;
            }

            date = localDate;

            if (date < this.dateRange.BeginDate)
            {
                this.AddValidation("End date must be greater than begin date", ValidationSeverity.Error, nameof(this.EndDate));
                return false;
            }

            return true;
        }

        public void OnNavigatedFrom(INavigationToken token)
        {
            // Nothing to do
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            var currentToken = (EditReportedTimeNavigationToken)token;

            this.dateRange = currentToken.DateRange;
        }
    }
}