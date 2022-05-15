using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.XF.Views.EditSpecficTime;
using System.Windows.Input;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimeViewModel : BindableBase, ISpecficExpectedTimeViewModel
    {
        private readonly INavigationService navigationService;
        private readonly SpecificExpectedTime specificExpectedTime;

        public SpecficExpectedTimeViewModel(
            SpecificExpectedTime specificExpectedTime,
            INavigationService navigationService)
        {
            this.specificExpectedTime = specificExpectedTime;
            this.navigationService = navigationService;

            this.EditCommand = new DelegateCommand(this.Edit);
        }

        private void Edit()
        {
            this.navigationService.PushAsync(new EditSpecficTimeNavigationToken(this.specificExpectedTime));
        }


        public string Date => this.specificExpectedTime.Date.ToShortDateString();

        public string Duration => this.specificExpectedTime.Duration.FormatTime();

        public ICommand EditCommand { get; }       

        }
    }