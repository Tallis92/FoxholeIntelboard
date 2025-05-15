using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

public enum WeaponType
{
    [Description("Standard weapon")]
    Rifle,
    [Description("Standard weapon with long range")]
    Long_Rifle,
    [Description("Standard weapon with more damage")]
    Heavy_Rifle,
    [Description("Automatic weapon with great damage")]
    Assault_Rifle,
    [Description("Secondary weapon")]
    Pistol,
    [Description("Powerful and close range weapon")]
    Shotgun,
    [Description("Long distance rifle")]
    Sniper_Rifle,
    [Description("Automatic small arm")]
    Submachine_Gun,
}

namespace IntelboardAPI.Models
{
    public class Weapon : CraftableItem
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
        [JsonPropertyName("specialProperties")]
        public List<string> SpecialProperties { get; set; }
        [JsonPropertyName("isTeched")]
        public bool IsTeched { get; set; } = false;

        public string GetWeaponType()
        {
            var weapon = WeaponType.GetType().GetField(WeaponType.ToString());
            var attribute = weapon?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? WeaponType.ToString();

        }

    }
}
