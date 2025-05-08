namespace FoxholeIntelboard.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int CraftableItemId { get; set; }
        public CraftableItem? CraftableItem { get; set; }
        public int? ResourceId { get; set; }
        public Resource? Resource { get; set; }
        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
    }
}
