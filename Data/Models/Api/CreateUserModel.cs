using System.Text.Json.Serialization;

namespace TestFramework_NET.Data.Models.Api
{
    internal class CreateUserModel
    {
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName("status")]
        public IEnumerable<BookModel>? Status { get; set; }
    }
}
