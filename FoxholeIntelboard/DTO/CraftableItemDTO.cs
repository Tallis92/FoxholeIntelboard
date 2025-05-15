using IntelboardAPI.Models;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DTO
{
    public class CraftableItemDTO
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cost")]
        public List<Cost> ProductionCost { get; set; } = new();
    }
}
