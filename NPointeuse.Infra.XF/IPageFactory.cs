using NPointeuse.Infra.Client;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    public interface IPageFactory
    {
        Page CreatePage(INavigationToken token);
    }
}
