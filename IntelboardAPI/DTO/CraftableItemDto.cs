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
        public List<CostDto> ProductionCost { get; set; } = new();
    }
    public class CostDto
    {

        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("resourceId")]
        public int? ResourceId { get; set; }
        [JsonPropertyName("materialId")]
        public int? MaterialId { get; set; }
       
    }
}
