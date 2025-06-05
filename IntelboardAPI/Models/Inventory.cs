using Azure.Core.Serialization;
using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public class Inventory
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cratedItems")]
        public List<CratedItem> CratedItems{ get; set; } = new();
        [JsonPropertyName("factionId")]
        public int? FactionId { get; set; }

    }
    public class CratedItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItem")]
        public CraftableItem CraftableItem { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("requiredAmount")]
        public int RequiredAmount { get; set; }
        [JsonPropertyName("description")]
        public string  Description { get; set; }

        // Ef Core always requires a parameterless constructor (public or protetect) to be able to create objects from the database
        public CratedItem() { }
        public CratedItem(CraftableItem item, int amount, int requiredAmount, string description, string name)
        {
            CraftableItem = item;
            Id = item.Id;
            Amount = amount;
            RequiredAmount = requiredAmount;
            Description = $"{amount} x crate(s) of {item.Name}";
        }

    }
}
