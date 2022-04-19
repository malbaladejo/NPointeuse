using NPointeuse.Infra.Client;
using NPointeuse.Infra.IOC;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    public interface IViewContainer
    {
        void Register<TToken, TView, TViewModel>()
            where TView : Page
            where TToken : INavigationToken;
    }
}
