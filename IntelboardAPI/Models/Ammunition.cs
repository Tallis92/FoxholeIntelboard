using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public enum DamageType
    {
        [Description("Deals light kinetic damage")]
        Kinetic,
        [Description("Deals heavy kinetic damage")]
        HeavyKinetic,
        [Description("Deals explosive damage")]
        Explosive,
        [Description("Deals high explosive damage")]
        HighExplosive,
        [Description("Deals armor piercing damage")]
        ArmorPiercing,
        [Description("Deals shrapnel damage")]
        Shrapnel,
        [Description("Deals poisionous gas damage")]
        Gas,
        [Description("Deals anti-tank explosive damage")]
        AntiTank,
        [Description("Deals anti-tank kinetic damage")]
        AntiTankKinetic,
        [Description("Deals demolition damage")]
        Demolition,
    }

    public enum AmmoProperties
    {
        [Display(Name = "AT-Suppressesion")]
        [Description("Suppresses enemy vehicles")]
        AT_Suppression,
        [Display(Name = "Armour Piercing")]
        [Description("Can penetrate armoured vehicles")]
        Armour_Piercing,
        [Display(Name = "Higher Penetration")]
        [Description("Higher chance to penetrate armoured vehicles at direct angles(To the sides/rear of the target) and at close range")]
        Higher_Penetration,
        [Display(Name = "Destroy Structures")]
        [Description("Can ruin Structures that have been severely damaged by artillery")]
        Destroy_Structures,
        [Display(Name = "Reduced Trenches")]
        [Description("Reduced damage against Trenches")]
        Reduced_Trenches,
        [Display(Name = "Increased Structures")]
        [Description("Increased damage against Field Structures")]
        Increased_Damage_To_Structures,
    }

    public class Ammunition : CraftableItem
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }

        [JsonPropertyName("damage")]
        public DamageType DamageType { get; set; }
        [JsonPropertyName("ammoProperties")]
        public List<AmmoProperties> AmmoProperties { get; set; }
        [JsonPropertyName("categoriId")]
        public int CategoryId { get; set; }

        public string GetDamageDescription()
        {
            var damage = DamageType.GetType().GetField(DamageType.ToString());
            var attribute = damage?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? DamageType.ToString();
        }

    }
}
