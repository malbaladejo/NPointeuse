using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.About
{
    internal class AboutNavigationToken : IIconNavigationToken
    {
        public string Title => "About";

        public string Icon => "icon_about.png";
    }
}
