using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace IntelboardCore.Models
{
    public abstract class CraftableItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("productionCost")]
        public List<Cost> ProductionCost { get; set; } = new();
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
