using System.ComponentModel.DataAnnotations.Schema;

namespace FoxholeIntelboard.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int CraftableItemId { get; set; } // Främmande nyckel till CraftableItem
        [ForeignKey("CraftableItemId")]
        public CraftableItem CraftableItem { get; set; }
    }
}
