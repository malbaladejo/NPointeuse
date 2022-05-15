using NPointeuse.Services;

namespace NPointeuse.XF.Views.SpecficExpected
{
    internal interface ISpecficExpectedTimeViewModelFactory
    {
        ISpecficExpectedTimeViewModel CreateInstance(SpecificExpectedTime specificExpectedTime);
    }
}