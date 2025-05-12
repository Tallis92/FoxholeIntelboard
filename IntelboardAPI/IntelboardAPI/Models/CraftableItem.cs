using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public abstract class CraftableItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cost")]
        public List<Cost> ProductionCost { get; set; } = new();
    }
}
