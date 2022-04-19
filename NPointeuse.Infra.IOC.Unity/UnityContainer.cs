using System;
using Unity;

namespace NPointeuse.Infra.IOC.Unity
{
    internal class Container : IContainer, IServiceLocator
    {
        private readonly UnityContainer container = new UnityContainer();

        public TService GetInstance<TService>() where TService : class
            => this.container.Resolve<TService>();

        public object GetInstance(Type type)
            => this.container.Resolve(type);

        public void Register(Type concreteType) => this.container.RegisterType(concreteType);

        public void Register(Type serviceType, Type implementationType) => this.container.RegisterType(serviceType, implementationType);

        public void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
         => this.container.RegisterType<TService, TImplementation>();

        public void Register<TConcrete>() where TConcrete : class
           => this.container.RegisterType<TConcrete>();

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
