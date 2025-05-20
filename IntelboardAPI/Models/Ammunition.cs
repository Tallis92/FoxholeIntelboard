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
        [Display(Name = "Armor Piercing")]
        [Description("Bye bye armor!")]
        ArmorPiercing,
        [Display(Name = "Colonial Attachments")]
        [Description("Bayonette equipable")]
        ColonialAttachments,
        [Display(Name = "Damage -15%")]
        [Description("This weapon deals -15% damage per shot")]
        Damage15Percent,
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
        public int CategoriId { get; set; }

        public string GetDamageDescription()
        {
            var damage = DamageType.GetType().GetField(DamageType.ToString());
            var attribute = damage?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? DamageType.ToString();
        }

    }
}
