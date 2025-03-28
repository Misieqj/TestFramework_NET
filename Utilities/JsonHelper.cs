using System.Text.Json;

namespace TestFramework_NET.Utilities
{
    public static class JsonHelper
    {
        public static async Task<T> LoadJsonAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Plik JSON nie istnieje: {filePath}");
            }

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Deserializacja zwróciła null.");
        }
    }

    /*
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // JSON
    {
        "name": "John Doe",
        "age": 30
    }

    Person person = await JsonHelper.LoadJsonAsync<Person>(filePath);
    */
}