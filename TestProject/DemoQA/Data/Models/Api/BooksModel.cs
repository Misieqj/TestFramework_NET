using System.Text.Json.Serialization;

namespace TestFramework_NET.TestProject.DemoQA.Data.Models.Api
{
    public class BooksModel
    {
        [JsonPropertyName("books")]
        public IEnumerable<BookModel>? Book { get; set; }
    }
}
