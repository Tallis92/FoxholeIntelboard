using System.Text.Json.Serialization;

namespace IntelboardAPI.DTO
{
    public class AmmunitionDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }

        [JsonPropertyName("damageType")]
        public string DamageType { get; set; }

        [JsonPropertyName("ammoProperties")]
        public List<string> AmmoProperties { get; set; }

        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
    }
}
