using System.Text.Json.Serialization;

namespace IntelboardCore.Models
{
    public class Cost
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonIgnore]
        [JsonPropertyName("craftableItem")]
        public CraftableItem? CraftableItem { get; set; }
        [JsonPropertyName("resourceId")]
        public int? ResourceId { get; set; }
        [JsonIgnore]
        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }
        [JsonPropertyName("materialId")]
        public int? MaterialId { get; set; }
        [JsonIgnore]
        [JsonPropertyName("material")]
        public Material? Material { get; set; }
    }
}
