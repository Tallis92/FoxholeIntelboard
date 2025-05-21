using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
