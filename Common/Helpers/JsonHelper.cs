using System.Text.Json;

namespace TestFramework_NET.Common.Helpers
{
    public static class JsonHelper
    {
        public static T LoadJson<T>(string file)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, file);
            string jsonContent = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<T>(jsonContent)
                ?? throw new InvalidDataException($"Unable to deserialize data from file: {filePath}");
        }
    }
}
