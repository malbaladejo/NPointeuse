using NPointeuse.Services;
using System.Windows.Input;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal interface ISpecficExpectedTimeViewModel
    {
        string Date { get; }
        string Duration { get; }
        ICommand EditCommand { get; }
    }
}