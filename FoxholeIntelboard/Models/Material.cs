using System.ComponentModel.DataAnnotations.Schema;

namespace FoxholeIntelboard.Models
{
    public class Material : CraftableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Resource RefinedFrom { get; set; }
        public int ConversionRate { get; set; }
    }
}
