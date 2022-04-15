namespace NPointeuse.Infra.IOC.SimpleInject
{
    public class SimpleInjectBootstrapper
    {
        public SimpleInjectBootstrapper()
        {

        }

        public IContainer Initialize()
        {
            var container = new SimpleInjectContainer();
            container.RegisterInstance<IContainer>(container);
            container.RegisterInstance<IServiceLocator>(container);

            return container;
        }
    }
}
