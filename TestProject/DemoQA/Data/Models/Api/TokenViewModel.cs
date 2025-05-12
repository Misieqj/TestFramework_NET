using System.Text.Json.Serialization;

namespace TestFramework_NET.TestProject.DemoQA.Data.Models.Api
{
    public class TokenViewModel
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
        [JsonPropertyName("expires")]
        public string? Expires { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("result")]
        public string? Result { get; set; }
    }
}
