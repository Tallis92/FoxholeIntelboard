namespace FoxholeIntelboard.Models
{
    public abstract class CraftableItem
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public List<Cost> ProductionCost { get; set; }
    }
}
