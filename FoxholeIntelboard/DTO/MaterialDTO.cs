using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DTO
{
    public class MaterialDTO
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
