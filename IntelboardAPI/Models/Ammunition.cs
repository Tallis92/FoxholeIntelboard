using Microsoft.OpenApi.Attributes;
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
        [Display("Warden Attachments")]
        [Description("Bayonette & Ospray grenade launcher equipable")]
        WardenAttachments,
        [Display("Colonial Attachments")]
        [Description("Bayonette equipable")]
        ColonialAttachments,
        [Display("Damage -15%")]
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
        public DamageType Damage { get; set; }
        [JsonPropertyName("specialProperties")]
        public List<AmmoProperties> SpecialProperties { get; set; }
        [JsonPropertyName("categoriId")]
        public int CategoriId { get; set; }

        public string GetDamageDescription()
        {
            var damage = Damage.GetType().GetField(Damage.ToString());
            var attribute = damage?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? Damage.ToString();
        }

        public string GetDisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            return member?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
        }
    }
}
