using System.Text.Json.Serialization;

namespace TestFramework_NET.TestProject.DemoQA.Data.Models.Api
{
    public class MessageModel
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }

    }
}
