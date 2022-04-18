using NPointeuse.Infra.IOC;

namespace NPointeuse.Services.LocalFile.Impl
{
    public class ServiceLocalFileBootstrapper
    {
        private readonly IContainer container;

        public ServiceLocalFileBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterSingleton<IDirectoryManager, DirectoryManager>();
            this.container.RegisterSingleton<ITimeDataService, LocalFileTimeDataService>();
            this.container.RegisterSingleton<ISpecificExpectedTimeDataService, LocalSpecificExpectedTimeDataService>();
            this.container.RegisterSingleton<IStandardExpectedTimeDataService, LocalFileStandardExpectedTimeDataService>();

            this.container.Register<ISerializer, JsonSerializerFacade>();
        }
    }
}
