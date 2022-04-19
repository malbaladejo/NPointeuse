using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.About
{
    internal class AboutNavigationToken : INavigationToken
    {
        public string Title => "About";
    }
}
