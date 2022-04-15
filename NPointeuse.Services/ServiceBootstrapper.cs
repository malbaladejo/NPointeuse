using NPointeuse.Infra.IOC;

namespace NPointeuse.Services
{
    public class ServiceBootstrapper
    {
        private readonly IContainer container;

        public ServiceBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterSingleton<IBusinessTimeService, BusinessTimeService>();
        }
    }
}
