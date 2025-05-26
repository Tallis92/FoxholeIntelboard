using IntelboardAPI.Models;
using System.Text.Json.Serialization;

namespace IntelboardAPI.DTO
{
    public class InventoryDto
    {
        [JsonPropertyName("inventoryId")]
        public Guid InventoryId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cratedItems")]
        public List<CratedItemDto> CratedItems { get; set; }
    }

    public class CratedItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class CratedItemInput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
