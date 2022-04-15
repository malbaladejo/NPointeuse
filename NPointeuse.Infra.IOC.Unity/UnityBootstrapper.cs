namespace NPointeuse.Infra.IOC.Unity
{
    public class UnityBootstrapper
    {
        public UnityBootstrapper()
        {

        }

        public IContainer Initialize()
        {
            var container = new Container();
            container.RegisterInstance<IContainer>(container);
            container.RegisterInstance<IServiceLocator>(container);

            return container;
        }
    }
}
