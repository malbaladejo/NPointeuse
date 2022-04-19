using NPointeuse.Infra.IOC;

namespace NPointeuse.Infra.XF
{
    public class InfraXFBootstrapper
    {
        private readonly IContainer container;

        public InfraXFBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            var pageFactory = this.container.GetInstance<PageFactory>();
            this.container.RegisterInstance<IViewContainer>(pageFactory);
            this.container.RegisterInstance<IPageFactory>(pageFactory);
        }
    }
}
