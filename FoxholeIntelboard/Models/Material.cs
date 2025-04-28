namespace FoxholeIntelboard.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Resource RefinedFrom { get; set; }
        public int ConversionRate { get; set; }
    }


}
