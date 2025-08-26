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

    [Display(Name = "Revolver")]
    [Description("Powerful secondary weapon")]
    Revolver,

    [Display(Name = "Shotgun")]
    [Description("Powerful and close range weapon")]
    Shotgun,

    [Display(Name = "Sniper Rifle")]
    [Description("Long distance rifle")]
    Sniper_Rifle,

    [Display(Name = "Submachine Gun")]
    [Description("Automatic small arm")]
    Submachine_Gun,

    [Display(Name = "Light Machine Gun")]
    [Description("Automatic weapon with low damage")]
    Light_Machine_Gun,

    [Display(Name = "Machine Gun")]
    [Description("Automatic weapon with high damage")]
    Machine_Gun,

    [Display(Name = "Mounted Anti-Tank Rifle")]
    [Description("Mounted weapon with high damage")]
    Mounted_Anti_Tank_Rifle,

    [Display(Name = "Mounted Infrantry Support Gun")]
    [Description("Mounted weapon with high damage")]
    Mounted_Infrantry_Support_Gun,

    [Display(Name = "Anti-Tank Rifle")]
    [Description("Weapon that can suppress tanks")]
    Anti_Tank_Rifle,

    [Display(Name = "Anti-Tank RPG")]
    [Description("Weapon that can penetrate armour")]
    Anti_Tank_RPG,

    [Display(Name = " Mounted Anti-Tank RPG")]
    [Description("Mounted weapon that can penetrate armour")]
    Mounted_Anti_Tank_RPG,

    [Display(Name = "Mounted Machine Gun")]
    [Description(" Mounted automatic weapon with high damage")]
    Mounted_Machine_Gun,

    [Display(Name = "RPG")]
    [Description("Launcher that deals explosive damage")]
    RPG,
}

public enum WeaponProperties
{
    [Display(Name = "None")]
    [Description("No properties")]
    None,

    [Display(Name = "Bayonette")]
    [Description("Allows buckhorn CCQ-18 (Bayonet) attatchment")]
    Bayonette,

    [Display(Name = "Ospreay")]
    [Description("Compatible ammo the attached Ospreay(Grenade Launcher): Green Ash Grenade, A3 Harpa Fragmentation Gernade, B2 Varsi Anti-Tank Grenade")]
    Ospreay,

    [Display(Name = "Damage -25%")]
    [Description("Equipped with a low velocity barrel that deals 25% less damage per shot")]
    DamageReduction25,

    [Display(Name = "Damage -36%")]
    [Description("Equipped with a low velocity barrel that deals 36% less damage per shot")]
    DamageReduction36,

    [Display(Name = "Damage +20%")]
    [Description("Equipped with a high velocity barrel that deals 20% etxra damage per shot")]
    DamageIncrease20,

    [Display(Name = "Suppressesion")]
    [Description("Suppresseion causes enemies to lose stability")]
    Suppression,

    [Display(Name = "AT-Suppressesion")]
    [Description("Suppresses enemy vehicles")]
    AT_Suppression,

    [Display(Name = "Gunner")]
    [Description("Allows the use of a gunner to fire the weapon")]
    Gunner,

    [Display(Name = "Armour Shredder")]
    [Description("Deals 200% damage to vehicle armour")]
    Armour_Shedder,

    [Display(Name = "Mountable")]
    [Description("" +
        "Can be attached to certain vehicles and tripods")]
    Mountable,

    [Display(Name = "Armour Piercing")]
    [Description("Can penetrate armoured vehicles")]
    Armour_Piercing,

    [Display(Name = "Higher Penetration")]
    [Description("Higher chance to penetrate armoured vehicles at direct angles(To the sides/rear of the target) and at close range")]
    Higher_Penetration,

    [Display(Name = "Damage +30%")]
    [Description("Equipped with a high velocity barrel that deals 30% extra damage per shot")]
    DamageIncrease30,

    [Display(Name = "Damage +150%")]
    [Description("Equipped with a high velocity barrel that deals 150% extra damage per shot")]
    DamageIncrease150,

    [Display(Name = "Damage -11%")]
    [Description("Equipped with a low velocity barrel that deals 11% less damage per shot")]
    DamageReduction11,

    [Display(Name = "Damage -20%")]
    [Description("Equipped with a low velocity barrel that deals 20% less damage per shot")]
    DamageReduction20,

    [Display(Name = "Damage +125%")]
    [Description("Equipped with a high velocity barrel that deals 125% extra damage per shot")]
    DamageIncrease125,

    [Display(Name = "Ignore cover -95%")]
    [Description("Accuracy bonus provided by cover is reduced by 95%")]
    IgnoreCover95,

    [Display(Name = "High Penetration")]
    [Description("High chance to penetrate armoured vehicles")]
    High_Penetration,

    [Display(Name = "Penetration +100%")]
    [Description("Additional 100% chance to penetrate armoured vehicles")]
    Penetration_Increase100,

    [Display(Name = "Damage +75%")]
    [Description("Equipped with a high velocity barrel that deals 75% extra damage per shot")]
    DamageIncrease75,

    [Display(Name = "Damage +55%")]
    [Description("Equipped with a high velocity barrel that deals 55% extra damage per shot")]
    DamageIncrease55,

    [Display(Name = "Damage +50%")]
    [Description("Equipped with a high velocity barrel that deals 50% extra damage per shot")]
    DamageIncrease50,

}

namespace IntelboardCore.Models
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
        [JsonPropertyName("weaponProperties")]
        public List<WeaponProperties> WeaponProperties { get; set; } = new();
        [JsonPropertyName("isTeched")]
        public bool IsTeched { get; set; } = false;
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        // Returns the name of the selected Enum
        public string GetWeaponName(Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name ?? value.ToString();
        }

    }
}
