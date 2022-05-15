using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using NPointeuse.XF.Views.EditSpecficTime;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimesBootstrapper
    {
        private readonly IContainer container;
        private readonly IViewContainer viewContainer;

        public SpecficExpectedTimesBootstrapper(IContainer container, IViewContainer viewContainer)
        {
            this.container = container;
            this.viewContainer = viewContainer;
        }

        public void Initialize()
        {
            this.container.Register<ISpecficExpectedTimeViewModelFactory, SpecficExpectedTimeViewModelFactory>();

            this.viewContainer.Register<SpecficExpectedTimesNavigationToken, SpecficExpectedTimesPage, SpecficExpectedTimesViewModel>();
            this.viewContainer.Register<EditSpecficTimeNavigationToken, EditSpecficTimePage, EditSpecficTimeViewModel>();
        }
    }
}
