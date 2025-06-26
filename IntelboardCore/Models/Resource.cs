using System.Text.Json.Serialization;

namespace IntelboardCore.Models
{
    public class Resource

    {
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
