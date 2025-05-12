using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestFramework_NET.Common.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Converts JSON file to an object of type T.
        /// </summary>
        public static T ObjectFromFile<T>(string file)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, file);
            string jsonContent = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<T>(jsonContent)
                ?? throw new InvalidDataException($"Unable to deserialize data from file: {filePath}");
        }

        /// <summary>
        /// Converts a JSON string to an object of type T.
        /// </summary>
        public static T ObjectFromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json)
                ?? throw new InvalidDataException($"Unable to deserialize data from JSON: {json}");
        }

        /// <summary>
        /// Converts an object to a dictionary with property names as keys and property values as values.
        /// Create property name from JsonPropertyName attribute if it exists.
        /// </summary>
        public static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.CanRead)
                .ToDictionary(
                    prop =>
                    {
                        var jsonAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                        return jsonAttr?.Name ?? prop.Name;
                    },
                    prop => prop.GetValue(obj) ?? ""
                );
        }
    }
}
