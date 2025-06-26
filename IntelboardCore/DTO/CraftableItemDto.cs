using IntelboardCore.Models;
using System.Text.Json.Serialization;

namespace IntelboardCore.DTO
{
    public class CraftableItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("productionCost")]
        public List<CostDto> ProductionCost { get; set; } = new();

        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }
    }
    public class CostDto
    {

        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("craftableItemId")]
        public int CraftableItemId { get; set; }
        [JsonPropertyName("resourceId")]
        public int? ResourceId { get; set; }
        [JsonPropertyName("materialId")]
        public int? MaterialId { get; set; }
        [JsonPropertyName("componentName")]
        public string ComponentName { get; set; }
       
    }

    public class WeaponDto
    {
        [JsonPropertyName("factionId")] // 0 is Wardens and 1 is Colonials to determine specific factions, with possibility to expand to more factions
        public int FactionId { get; set; }
        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }
        [JsonPropertyName("weaponType")]
        public WeaponType WeaponType { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("ammunitionId")] //This is the ammunition type the weapon uses
        public int AmmunitionId { get; set; }
        [JsonPropertyName("weaponProperties")]
        public List<WeaponProperties> WeaponProperties { get; set; } = new();
        [JsonPropertyName("isTeched")]
        public bool IsTeched { get; set; } = false;
        [JsonPropertyName("categoriId")]
        public int CategoryId { get; set; }


    }
}
