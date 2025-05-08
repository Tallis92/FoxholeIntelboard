using System.ComponentModel.DataAnnotations.Schema;

namespace FoxholeIntelboard.Models
{
    public class Material : CraftableItem
    {
        public int CrateAmount { get; set; }

    }
}
