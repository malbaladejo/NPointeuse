namespace NPointeuse.Services.LocalFile.Impl
{
    internal interface ISerializer
    {
        void Serialize(object data, string filePath);
        T Deserialize<T>(string filePath);
    }
}
