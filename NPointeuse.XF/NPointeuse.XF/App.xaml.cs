using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using System;
using Xamarin.Forms;

namespace NPointeuse.XF
{
    public partial class App : Application
    {
        private IContainer container;
        private IPageFactory pageFactory;

        public App()
        {
            InitializeComponent();
            this.InitializeContainer();
            this.InitializeMainPage();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void InitializeMainPage()
        {
            MainPage = new AppShell(this.pageFactory);
        }

        private void InitializeContainer()
        {
            var bootstrapper = new Bootstrapper();

            this.container = bootstrapper.Initialize();

            this.pageFactory = this.container.GetInstance<IPageFactory>();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
