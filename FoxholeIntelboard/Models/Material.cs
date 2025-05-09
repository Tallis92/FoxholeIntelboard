using System.ComponentModel.DataAnnotations.Schema;

namespace FoxholeIntelboard.Models
{
    public class Material : CraftableItem
    {
        public int CrateAmount { get; set; }
        public bool? TechMaterial { get; set; }
        public bool? LargeMaterial { get; set; }
        public bool? FacilityMade { get; set; }

    }
}
