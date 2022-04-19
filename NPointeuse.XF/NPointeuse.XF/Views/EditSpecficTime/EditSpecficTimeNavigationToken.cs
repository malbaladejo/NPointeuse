using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;

namespace NPointeuse.XF.Views.EditSpecficTime
{
    internal class EditSpecficTimeNavigationToken : INavigationToken
    {
        public EditSpecficTimeNavigationToken(SpecificExpectedTime specificExpectedTime)
        {
            SpecificExpectedTime = specificExpectedTime;
        }

        public string Title => "Edit reported time";

        public SpecificExpectedTime SpecificExpectedTime { get; }
    }
}