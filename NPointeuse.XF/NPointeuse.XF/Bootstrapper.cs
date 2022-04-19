using NPointeuse.Infra.XF;
using NPointeuse.Infra.IOC;
using NPointeuse.Infra.IOC.Unity;
using NPointeuse.Infra.XF;
using NPointeuse.Services;
using NPointeuse.Services.LocalFile.Impl;

namespace NPointeuse.XF
{
    internal class Bootstrapper
    {
        public IContainer Initialize()
        {
            var bootstrapper = new UnityBootstrapper();
            var container = bootstrapper.Initialize();

            container.GetInstance<ServiceBootstrapper>().Initialize();
            container.GetInstance<ServiceLocalFileBootstrapper>().Initialize();
            
            container.GetInstance<InfraXFBootstrapper>().Initialize();
           
            container.GetInstance<ViewsBootstrapper>().Initialize();

            return container;
        }
    }
}
