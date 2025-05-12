using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public class Cost
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("craftableitem id")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("craftable item")]
        public CraftableItem? CraftableItem { get; set; }
        [JsonPropertyName("resource id")]
        public int? ResourceId { get; set; }
        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }
        [JsonPropertyName("material id")]
        public int? MaterialId { get; set; }
        [JsonPropertyName("material")]
        public Material? Material { get; set; }
    }
}
