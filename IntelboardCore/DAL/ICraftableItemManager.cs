using IntelboardCore.Models;
using IntelboardCore.DTO;

namespace IntelboardCore.DAL
{
    public interface ICraftableItemManager
    {
        Task<List<CraftableItemDto>> GetCraftableItemsAsync();
    }
}
