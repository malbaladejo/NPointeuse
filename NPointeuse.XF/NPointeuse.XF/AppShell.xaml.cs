using NPointeuse.Infra.Client;
using NPointeuse.Infra.IOC;
using NPointeuse.Infra.XF;
using NPointeuse.XF.Views.About;
using NPointeuse.XF.Views.Configuration;
using NPointeuse.XF.Views.Home;
using NPointeuse.XF.Views.SpecficExpected;
using NPointeuse.XF.Views.StandardExpected;
using System;
using Xamarin.Forms;

namespace NPointeuse.XF
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        //private IContainer container;
        private IPageFactory pageFactory;
        private readonly DelegateCommand<INavigationToken> openCommand;
        private INavigationService navigationService;

        public AppShell(IPageFactory pageFactory)
        {
            InitializeComponent();

            this.pageFactory = pageFactory;
            this.navigationService = new NavigationService(this.Navigation, pageFactory);
            this.openCommand = new DelegateCommand<INavigationToken>(this.Open);

            this.AddShelItem(new HomeNavigationToken());

            this.AddMenuItem(new StandardExpectedTimesNavigationToken());
            this.AddMenuItem(new SpecficExpectedTimesNavigationToken());
            this.AddMenuItem(new AboutNavigationToken());
        }

        private void Open(INavigationToken token)
        {
            this.FlyoutIsPresented = false;   
            this.navigationService.PushAsync(token);
        }

        private void AddShelItem(INavigationToken token)
        {
            var page = pageFactory.CreatePage(token);

            this.Items.Add(new ShellContent() { Content = page });
        }

        private void AddMenuItem(IIconNavigationToken token)
        {
            var menuItem = new MenuItem
            {
                Text = token.Title,
                IconImageSource = token.Icon,
                Command = this.openCommand,
                CommandParameter = token
            };

            this.Items.Add(menuItem);
        }
    }
}
