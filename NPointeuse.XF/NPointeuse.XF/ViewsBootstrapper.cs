using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using NPointeuse.XF.ViewModels;
using NPointeuse.XF.Views;
using NPointeuse.XF.Views.About;
using NPointeuse.XF.Views.Configuration;
using NPointeuse.XF.Views.EditReportedTime;
using NPointeuse.XF.Views.EditSpecficTime;
using NPointeuse.XF.Views.Home;
using NPointeuse.XF.Views.ReportedTimes;
using NPointeuse.XF.Views.SpecficExpected;
using NPointeuse.XF.Views.StandardExpected;

namespace NPointeuse.XF
{
    internal class ViewsBootstrapper
    {
        private readonly IContainer container;
        private readonly IViewContainer viewContainer;

        public ViewsBootstrapper(IContainer container, IViewContainer viewContainer)
        {
            this.container = container;
            this.viewContainer = viewContainer;
        }

        public void Initialize()
        {
            viewContainer.Register<HomeNavigationToken, HomePage, HomeViewModel>();

            viewContainer.Register<ReportedTimesNavigationToken, ReportedTimesPage, ReportedTimesViewModel>();
            viewContainer.Register<EditReportedTimeNavigationToken, EditReportedTimePage, EditReportedTimeViewModel>();

            viewContainer.Register<StandardExpectedTimesNavigationToken, StandardExpectedPage, StandardExpectedTimesViewModel>();

            viewContainer.Register<SpecficExpectedTimesNavigationToken, SpecficExpectedTimesPage, SpecficExpectedTimesViewModel>();
            viewContainer.Register<EditSpecficTimeNavigationToken, EditSpecficTimePage, EditSpecficTimeViewModel>();

            viewContainer.Register<AboutNavigationToken, AboutPage, AboutViewModel>();

            container.GetInstance<ConfigurationBootstrapper>().Initialize();
        }
    }
}
