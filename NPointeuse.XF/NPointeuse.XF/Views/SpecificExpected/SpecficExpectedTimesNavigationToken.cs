using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal class SpecficExpectedTimesNavigationToken : IIconNavigationToken
    {
        public string Title => "Specfic Expected Times";

        public string Icon => "icon_gear.png";
    }
}