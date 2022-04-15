namespace NPointeuse.Infra.IOC
{
    public interface IServiceLocator
    {
        TService GetInstance<TService>() where TService : class;
    }
}
