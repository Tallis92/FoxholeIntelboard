namespace IntelboardAPI.Models
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public List<CratedItem> CratedItem { get; set; } = new();
        // public UserId UserId { get; set; }

    }
    public class CratedItem
    {
        public int Id { get; set; }
        public CraftableItem CraftableItem { get; set; }
        public int Amount { get; set; }
        public string  Description { get; set; }

        // EF Core kräver alltid en parameterlös konstruktor (offentlig eller skyddad) för att kunna skapa objekt från databasen.
        public CratedItem() { }
        public CratedItem(CraftableItem item, int amount, string description)
        {
            CraftableItem = item;
            Id = item.Id;
            Amount = amount;
            Description = $"Crate containing {amount} of {item.Name}";
        }

    }
}
