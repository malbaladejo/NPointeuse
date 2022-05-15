using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.StandardExpected
{
    internal class StandardExpectedTimesNavigationToken : IIconNavigationToken
    {
        public string Title => "Standard Expected Times";

        public string Icon => "icon_gear.png";
    }
}