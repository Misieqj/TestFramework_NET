using System.Text.Json.Serialization;

namespace TestFramework_NET.TestProject.DemoQA.Data.Models.Api
{
    public class BookModel
    {
        [JsonPropertyName("isbn")]
        public string? Isbn { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("subTitle")]
        public string? SubTitle { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("publish_date")]
        public string? PublishDate { get; set; }
        [JsonPropertyName("publisher")]
        public string? Publisher { get; set; }
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("website")]
        public string? Website { get; set; }
    }
}
