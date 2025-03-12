namespace FoxholeIntelboard.Models
{
    public class RefinedMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RawMaterial RefinedFrom { get; set; }
        public int ConversionRate { get; set; }
    }


}
