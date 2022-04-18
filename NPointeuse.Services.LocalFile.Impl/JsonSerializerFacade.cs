using System.IO;
using System.Text.Json;

namespace NPointeuse.Services.LocalFile.Impl
{
    internal class JsonSerializerFacade : ISerializer
    {
        public T Deserialize<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }

        public void Serialize(object data, string filePath)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
