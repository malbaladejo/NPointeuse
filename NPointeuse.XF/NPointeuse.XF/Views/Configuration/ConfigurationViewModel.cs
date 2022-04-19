using NPointeuse.Infra.Client;

namespace NPointeuse.XF.Views.Configuration
{
    internal class ConfigurationViewModel : BindableBase, INavigationAware
    {
        public ConfigurationViewModel(
            IConfigurationStandardTimeViewModel configurationStandardTimeViewModel,
            IConfigurationSpecificTimeViewModel configurationSpecificTimeViewModel)
        {
            this.ConfigurationStandardTimeViewModel = configurationStandardTimeViewModel;
            this.ConfigurationSpecificTimeViewModel = configurationSpecificTimeViewModel;
        }

        public IConfigurationStandardTimeViewModel ConfigurationStandardTimeViewModel { get; }

        public IConfigurationSpecificTimeViewModel ConfigurationSpecificTimeViewModel { get; }

        public void OnNavigatedFrom(INavigationToken token)
        {
            this.ConfigurationStandardTimeViewModel.OnNavigatedFrom(token);
            this.ConfigurationSpecificTimeViewModel.OnNavigatedFrom(token);
        }

        public void OnNavigatedTo(INavigationToken token)
        {
            this.ConfigurationStandardTimeViewModel.OnNavigatedTo(token);
            this.ConfigurationSpecificTimeViewModel.OnNavigatedTo(token);
        }
    }
}
