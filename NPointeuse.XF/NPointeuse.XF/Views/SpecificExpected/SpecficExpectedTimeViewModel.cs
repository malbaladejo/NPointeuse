using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimeViewModel : BindableBase, ISpecficExpectedTimeViewModel
    {
        private readonly SpecificExpectedTime specificExpectedTime;
        private readonly INavigationService navigationService;

        public SpecficExpectedTimeViewModel(
            SpecificExpectedTime specificExpectedTime,
            INavigationService navigationService)
        {
            this.specificExpectedTime = specificExpectedTime;
            this.navigationService = navigationService;
        }

        public string Date => this.specificExpectedTime.Date.ToShortDateString();

        public string Duration => this.specificExpectedTime.Duration.FormatTime();
    }


    internal interface ISpecficExpectedTimeViewModelFactory { 
    }
}