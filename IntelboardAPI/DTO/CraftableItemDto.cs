using IntelboardAPI.Models;
using System.Text.Json.Serialization;

namespace IntelboardAPI.DTO
{
    public class CraftableItemDto
    {
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("productionCost")]
        public List<Cost> ProductionCost { get; set; } = new();
    }
}
