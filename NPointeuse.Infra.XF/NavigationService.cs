using NPointeuse.Infra.Client;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    internal class NavigationService : INavigationService
    {
        private readonly INavigation navigation;
        private readonly IPageFactory pageFactory;

        public NavigationService(INavigation navigation, IPageFactory pageFactory)
        {
            this.navigation = navigation;
            this.pageFactory = pageFactory;
        }

        public Task<Page> PopAsync() => this.navigation.PopAsync();

        public Task<Page> PopAsync(bool animated) => this.navigation.PopAsync(animated);

        public Task<Page> PopModalAsync() => this.navigation.PopModalAsync();

        public Task<Page> PopModalAsync(bool animated) => this.navigation.PopModalAsync(animated);

        public Task PopToRootAsync() => this.navigation.PopToRootAsync();

        public Task PopToRootAsync(bool animated) => this.navigation.PopToRootAsync(animated);

        public Task PushAsync(INavigationToken token)
        {
            var page = pageFactory.CreatePage(token);
            return navigation.PushAsync(page);
        }

        public Task PushAsync(INavigationToken token, bool animated)
        {
            var page = pageFactory.CreatePage(token);
            return navigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(INavigationToken token)
        {
            var page = pageFactory.CreatePage(token);
            return navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(INavigationToken token, bool animated)
        {
            var page = pageFactory.CreatePage(token);
            return navigation.PushModalAsync(page, animated);
        }

        //public void RemovePage(INavigationToken token)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
