using System;

namespace NPointeuse.Infra.Client
{
    public interface IConfirmationAware
    {
        void OnConfirmNavigatedFrom(INavigationToken token, Action<bool> canNavigate);
    }
}
