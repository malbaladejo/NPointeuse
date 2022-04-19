using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.Configuration
{
    internal class ConfigurationBootstrapper
    {
        private readonly IContainer container;
        private readonly IViewContainer viewContainer;

        public ConfigurationBootstrapper(IContainer container, IViewContainer viewContainer)
        {
            this.container = container;
            this.viewContainer = viewContainer;
        }

        public void Initialize()
        {     
            viewContainer.Register<ConfigurationNavigationToken, ConfigurationPage, ConfigurationViewModel>();

            container.Register<IConfigurationStandardTimeViewModel, ConfigurationStandardTimeViewModel>();
            container.Register<IConfigurationSpecificTimeViewModel, ConfigurationSpecificTimeViewModel>();
        }
    }
}
