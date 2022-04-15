using System;

namespace NPointeuse.Infra.IOC
{
    public interface IContainer
    {
        TService GetInstance<TService>() where TService : class;

        void Register(Type concreteType);

        void Register(Type serviceType, Type implementationType);

        void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService;

        void RegisterInstance<TService>(TService instance) where TService : class;

        void Register<TConcrete>() where TConcrete : class;

        void RegisterInstance(Type serviceType, object instance);

        void RegisterSingleton(Type serviceType, Type implementationType);

        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
    }
}
