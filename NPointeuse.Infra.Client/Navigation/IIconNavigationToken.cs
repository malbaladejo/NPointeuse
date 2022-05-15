namespace NPointeuse.Infra.Client
{
    public interface IIconNavigationToken: INavigationToken
    {
        string Icon { get; }
    }
}
