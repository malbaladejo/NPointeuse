using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.Home
{
    internal class HomeNavigationToken : INavigationToken
    {
        string INavigationToken.Title => "Home";
    }
}