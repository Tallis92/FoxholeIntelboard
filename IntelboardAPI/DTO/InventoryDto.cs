namespace IntelboardAPI.DTO
{
    public class InventoryDto
    {
        public string Name { get; set; }
        public List<CratedItemDto> CratedItems { get; set; }
    }

    public class CratedItemDto
    {
        public int CraftableItemId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
