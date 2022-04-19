using NPointeuse.Infra.IOC;
using NPointeuse.Infra.IOC.Unity;
using NPointeuse.Services;
using NPointeuse.Services.LocalFile.Impl;
using NPointeuse.Services.Mock;

namespace NPointeuse
{
    internal class Bootstrapper
    {
        public IContainer Initialize()
        {
            var bootstrapper = new UnityBootstrapper();
            var container = bootstrapper.Initialize();
            
            container.GetInstance<ServiceBootstrapper>().Initialize();
            //   container.GetInstance<ServiceMockBootstrapper>().Initialize();
            container.GetInstance<ServiceLocalFileBootstrapper>().Initialize();            

            container.Register<IConsoleWriter, ConsoleWriter>();
            container.Register<IConsoleProgressBar, ConsoleProgressBar>();
            
            return container;
        }
    }
}
