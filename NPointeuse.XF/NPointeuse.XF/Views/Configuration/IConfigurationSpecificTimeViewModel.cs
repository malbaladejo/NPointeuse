using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using System.Windows.Input;

namespace NPointeuse.XF.Views.Configuration
{
    internal interface IConfigurationSpecificTimeViewModel : INavigationAware
    {
        ICommand ShowAllCommand { get; }

        string Label { get; }
    }
}
