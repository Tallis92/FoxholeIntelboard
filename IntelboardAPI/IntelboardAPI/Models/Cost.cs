using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public class Cost
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("craftableItem")]
        public CraftableItem? CraftableItem { get; set; }
        [JsonPropertyName("resourceId")]
        public int? ResourceId { get; set; }
        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }
        [JsonPropertyName("materialId")]
        public int? MaterialId { get; set; }
        [JsonPropertyName("material")]
        public Material? Material { get; set; }
    }
}
