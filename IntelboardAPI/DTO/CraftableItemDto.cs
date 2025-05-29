using IntelboardAPI.Models;
using System.Text.Json.Serialization;

namespace IntelboardAPI.DTO
{
    public class CraftableItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("productionCost")]
        public List<CostDto> ProductionCost { get; set; } = new();

        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }
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
        [JsonPropertyName("componentName")]
        public string ComponentName { get; set; }
       
    }
}
