using NPointeuse.Infra.IOC;
using NPointeuse.Infra.IOC.Unity;
using NPointeuse.Services;
using NPointeuse.Services.Mock;

namespace NPointeuse
{
    internal class Bootstrapper
    {
        public IContainer Initialize()
        {
            var simpleInjectBootstrapper = new UnityBootstrapper();
            var container = simpleInjectBootstrapper.Initialize();
            
            container.GetInstance<ServiceBootstrapper>().Initialize();
            container.GetInstance<ServiceMockBootstrapper>().Initialize();

            container.Register<IConsoleWriter, ConsoleWriter>();
            container.Register<IConsoleProgressBar, ConsoleProgressBar>();
            
            return container;
        }
    }
}
