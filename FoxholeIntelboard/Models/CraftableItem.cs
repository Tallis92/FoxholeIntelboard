using System.Collections.Generic;

namespace FoxholeIntelboard.Models
{
    public abstract class CraftableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cost> ProductionCost { get; set; } = new();

    }
}
