using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace IntelboardCore.DTO
{
    public class InventoryDto
    {
        [JsonPropertyName("inventoryId")]
        public Guid InventoryId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [BindNever]
        [JsonPropertyName("cratedItems")]
        public List<CratedItemDto> CratedItems { get; set; }
        [JsonPropertyName("factionId")]
        public int? FactionId { get; set; }
    }

    public class CratedItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("requiredAmount")]
        public int RequiredAmount { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("componentName")]
        public string? ComponentName { get; set; }
    }

    public class CratedItemInput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("requiredAmount")]
        public int RequiredAmount { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
