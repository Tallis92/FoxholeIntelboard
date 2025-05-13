using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace IntelboardAPI.Models
{
    public enum DamageType
    {
        [Description("Deals light kinetic damage")]
        Kinetic,
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
        [Description("Deals demolition damage")]
        Demolition,
    }

    public class Ammunition : CraftableItem
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("crateAmount")]
        public int CrateAmount { get; set; }

        [JsonPropertyName("damage")]
        public DamageType Damage { get; set; }

        public string GetDamageDescription()
        {
            var damage = Damage.GetType().GetField(Damage.ToString());
            var attribute = damage?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? Damage.ToString();
        }
    }
}
