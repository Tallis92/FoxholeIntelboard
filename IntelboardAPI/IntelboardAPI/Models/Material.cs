using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public class Material : CraftableItem
    {
        [JsonPropertyName("crate amount")]
        public int CrateAmount { get; set; }
        [JsonPropertyName("Tech Material")]
        public bool? TechMaterial { get; set; }
        [JsonPropertyName("Large Material")]
        public bool? LargeMaterial { get; set; }
        [JsonPropertyName("Facilitymade")]
        public bool? FacilityMade { get; set; }

    }
}
