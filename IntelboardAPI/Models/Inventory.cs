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
        // public UserId UserId { get; set; }

    }
    public class CratedItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItem")]
        public CraftableItem CraftableItem { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("description")]
        public string  Description { get; set; }


        // EF Core kräver alltid en parameterlös konstruktor (offentlig eller skyddad) för att kunna skapa objekt från databasen.
        public CratedItem() { }
        public CratedItem(CraftableItem item, int amount, string description, string name)
        {
            CraftableItem = item;
            Id = item.Id;
            Amount = amount;
            Description = $"Crate containing {amount} of {item.Name}";
        }

    }
}
