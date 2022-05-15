using NPointeuse.Infra.XF;
using NPointeuse.Services;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimeViewModelFactory : ISpecficExpectedTimeViewModelFactory
    {
        private readonly INavigationService navigationService;

        public SpecficExpectedTimeViewModelFactory(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public ISpecficExpectedTimeViewModel CreateInstance(SpecificExpectedTime specificExpectedTime)
            => new SpecficExpectedTimeViewModel(specificExpectedTime, this.navigationService);
    }
}