using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using System.Windows.Input;

namespace NPointeuse.XF.Views.Configuration
{
    internal interface IConfigurationStandardTimeViewModel : INavigationAware
    {
        ICommand EditCommand { get; }

        string Label { get; }
    }
}
