using System.Text.Json.Serialization;

namespace TestFramework_NET.Data.Models.Api
{
    public class RegisterViewModel
    {
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
