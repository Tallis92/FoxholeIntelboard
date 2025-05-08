using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace FoxholeIntelboard.Models
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
        public string? Description { get; set; }
        public int CrateAmount { get; set; }
        public List<string>? SpecialProperties { get; set; }
        public DamageType Damage { get; set; }

        public string GetDamageDescription()
        {
            var damage = Damage.GetType().GetField(Damage.ToString());
            var attribute = damage?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? Damage.ToString();
        }
    }
}