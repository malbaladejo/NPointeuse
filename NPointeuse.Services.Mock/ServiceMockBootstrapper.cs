using NPointeuse.Infra.IOC;

namespace NPointeuse.Services.Mock
{
    public class ServiceMockBootstrapper
    {
        private readonly IContainer container;

        public ServiceMockBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterSingleton<ITimeDataService, TimeDataServiceMock>();
            this.container.RegisterSingleton<ISpecificExpectedTimeDataService, SpecificExpectedTimeDataServiceMock>();
            this.container.RegisterSingleton<IStandardExpectedTimeDataService, StandardExpectedTimeDataServiceMock>();
        }
    }
}
