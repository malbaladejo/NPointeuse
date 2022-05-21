using NPointeuse.Infra.Client;
using NPointeuse.Infra.IOC;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    internal class PageFactory : IViewContainer, IPageFactory
    {
        private readonly IContainer container;

        private readonly Dictionary<Type, Type> viewMapping = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> viewModelMapping = new Dictionary<Type, Type>();

        public PageFactory(IContainer container)
        {
            this.container = container;
        }

        public Page CreatePage(INavigationToken token)
        {
            if (!this.viewMapping.ContainsKey(token.GetType()))
                throw new InvalidOperationException($"No view register for token {token.GetType().FullName}.");
          
            var currentView = CreateView(token);
            var currentViewModel = CreateViewModel(token);

            this.NavigateToView(token, currentView);
            this.NavigateToView(token, currentViewModel);

            currentView.BindingContext = currentViewModel;
            return currentView;
        }

        private Page CreateView(INavigationToken token)
        {
            try
            {
                var viewType = viewMapping[token.GetType()];
                var view = (Page)this.container.GetInstance(viewType);

                this.container.RegisterInstance<INavigationService>(new NavigationService(view.Navigation, this));

                return view;
            }catch(Exception e)
            {
                throw;
            }
        }


        private object CreateViewModel(INavigationToken token)
        {
            var viewType = viewModelMapping[token.GetType()];
            var view = this.container.GetInstance(viewType);

            return view;
        }

        //private void NavigateFromCurrentView(INavigationToken token)
        //{
        //    this.Bind(this.currentView, null);

        //    this.NavigateFromView(token, this.currentView);
        //    this.DisposeCurrentView(this.currentView);

        //    this.NavigateFromView(token, this.currentViewModel);
        //    this.DisposeCurrentView(this.currentViewModel);
        //}

        private void NavigateFromView(INavigationToken token, object target)
        {
            if (target is INavigationAware viewNavigationAware)
                viewNavigationAware.OnNavigatedFrom(token);
        }

        private void NavigateToView(INavigationToken token, object target)
        {
            if (target is INavigationAware viewNavigationAware)
                viewNavigationAware.OnNavigatedTo(token);
        }

        private void DisposeCurrentView(object target)
        {
            if (target is IDisposable viewDisposable)
                viewDisposable.Dispose();
        }

        public void Register<TToken, TView, TViewModel>()
            where TView : Page
            where TToken : INavigationToken
        {
            this.RegisterToken<TToken, TView>(viewMapping);
            this.RegisterToken<TToken, TViewModel>(viewModelMapping);
        }

        private void RegisterToken<TToken, T>(Dictionary<Type, Type> mapping) where TToken : INavigationToken
        {
            if (mapping.ContainsKey(typeof(TToken)))
                throw new InvalidOperationException($"The token {typeof(TToken).FullName} is allready registered.");

            mapping[typeof(TToken)] = typeof(T);
        }
    }
}
