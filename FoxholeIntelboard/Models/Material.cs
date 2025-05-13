using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.Models
{
    public class Material : CraftableItem
    {
        [JsonPropertyName("crateAmount")]

        public int CrateAmount { get; set; }
        [JsonPropertyName("TechMaterial")]
        public bool? TechMaterial { get; set; }
        [JsonPropertyName("LargeMaterial")]

        public bool? LargeMaterial { get; set; }
        [JsonPropertyName("Facilitymade")]

        public bool? FacilityMade { get; set; }

    }
}
