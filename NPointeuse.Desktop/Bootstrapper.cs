using NPointeuse.Infra.IOC;
using NPointeuse.Infra.IOC.Unity;
using NPointeuse.Services;
using NPointeuse.Services.LocalFile.Impl;

namespace NPointeuse.Desktop
{
    internal class Bootstrapper
    {
        public IContainer Initialize()
        {
            var simpleInjectBootstrapper = new UnityBootstrapper();
            var container = simpleInjectBootstrapper.Initialize();

            container.GetInstance<ServiceBootstrapper>().Initialize();
            container.GetInstance<ServiceLocalFileBootstrapper>().Initialize();

            return container;
        }
    }
}
