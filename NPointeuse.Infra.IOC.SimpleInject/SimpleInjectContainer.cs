using SimpleInjector;
using System;

namespace NPointeuse.Infra.IOC.SimpleInject
{
    internal class SimpleInjectContainer : IContainer, IServiceLocator
    {
        private readonly Container container = new Container();

        public TService GetInstance<TService>() where TService : class
            => this.container.GetInstance<TService>();

        public void Register(Type concreteType) => this.container.Register(concreteType);

        public void Register(Type serviceType, Type implementationType) => this.container.Register(serviceType, implementationType);

        public void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
         => this.container.Register<TService, TImplementation>();
       
        public void Register<TConcrete>() where TConcrete : class 
           => this.container.Register<TConcrete>();

        public void RegisterInstance<TService>(TService instance) where TService : class
            => this.container.RegisterInstance(instance);

        public void RegisterInstance(Type serviceType, object instance)
            => this.container.RegisterInstance(serviceType, instance);

        public void RegisterSingleton(Type serviceType, Type implementationType)
            => this.container.RegisterSingleton(serviceType, implementationType);

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => this.container.RegisterSingleton<TService, TImplementation>();
    }
}
