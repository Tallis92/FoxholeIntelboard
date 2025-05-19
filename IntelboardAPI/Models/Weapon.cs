using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

public enum WeaponType
{
    [Display(Name = "Rifle")]
    [Description("Standard weapon")]
    Rifle,
    [Display(Name = "Long Rifle")]
    [Description("Standard weapon with long range")]
    Long_Rifle,
    [Display(Name = "Heavy Rifle")]
    [Description("Standard weapon with more damage")]
    Heavy_Rifle,
    [Display(Name = "Assault Rifle")]
    [Description("Automatic weapon with great damage")]
    Assault_Rifle,
    [Display(Name = "Pistol")]
    [Description("Secondary weapon")]
    Pistol,
    [Display(Name = "Shotgun")]
    [Description("Powerful and close range weapon")]
    Shotgun,
    [Display(Name = "Sniper Rifle")]
    [Description("Long distance rifle")]
    Sniper_Rifle,
    [Display(Name = "Submachine Gun")]
    [Description("Automatic small arm")]
    Submachine_Gun,
}

public enum WeaponProperties
{
    [Display(Name = "Warden Attachments")]
    [Description("Bayonette & Ospray grenade launcher equipable")]
    WardenAttachments,
    [Display(Name = "Colonial Attachments")]
    [Description("Bayonette equipable")]
    ColonialAttachments,
    [Display(Name = "Damage -15%")]
    [Description("This weapon deals -15% damage per shot")]
    Damage15Percent,

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
        public List<WeaponProperties> SpecialProperties { get; set; } = new();
        [JsonPropertyName("isTeched")]
        public bool IsTeched { get; set; } = false;
        [JsonPropertyName("categoriId")]
        public int CategoriId { get; set; }

        public string GetWeaponType()
        {
            var weapon = WeaponType.GetType().GetField(WeaponType.ToString());
            var attribute = weapon?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? WeaponType.ToString();

        }

    }
}
