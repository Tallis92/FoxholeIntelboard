using System.Text.Json.Serialization;

namespace IntelboardCore.Models
{
    public class Material : CraftableItem
    {
        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }
        [JsonPropertyName("techMaterial")]
        public bool? TechMaterial { get; set; }
        [JsonPropertyName("largeMaterial")]
        public bool? LargeMaterial { get; set; }
        [JsonPropertyName("facilitymade")]
        public bool? FacilityMade { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

    }
}
