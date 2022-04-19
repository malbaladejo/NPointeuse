using NPointeuse.Infra.Client;
using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using NPointeuse.XF.Views.About;
using NPointeuse.XF.Views.Configuration;
using NPointeuse.XF.Views.Home;

using Xamarin.Forms;

namespace NPointeuse.XF
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly IContainer container;
        private readonly IPageFactory pageFactory;
        public AppShell()
        {
            InitializeComponent();

            var bootstrapper = new Bootstrapper();

            this.container = bootstrapper.Initialize();

            this. pageFactory = this.container.GetInstance<IPageFactory>();
            this.CreateTab(new HomeNavigationToken());
            this.CreateTab(new ConfigurationNavigationToken());
            this.CreateTab(new AboutNavigationToken());
        }

        private void CreateTab(INavigationToken token)
        {
            var home = pageFactory.CreatePage(token );

            var shellSection = new ShellSection
            {
                Title = token.Title
            };

            shellSection.Items.Add(new ShellContent() { Content = home });

            tabBar.Items.Add(shellSection);
        }
    }
}
