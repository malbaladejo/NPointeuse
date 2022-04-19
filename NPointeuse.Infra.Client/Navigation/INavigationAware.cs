namespace NPointeuse.Infra.Client
{
    public interface INavigationAware
    {
        void OnNavigatedTo(INavigationToken token);

        void OnNavigatedFrom(INavigationToken token);
    }
}
