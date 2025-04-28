using System.Text.Json;

namespace TestFramework_NET.Common
{
    public static class JsonHelper
    {
        public static T LoadJsonAsync<T>(string file)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, file);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not fount: {filePath}");
            }
            string jsonContent = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<T>(jsonContent)
                ?? throw new InvalidDataException($"Unable to deserialize data from file: {filePath}");
        }
    }
}
