using IntelboardCore.Models;
using IntelboardCore.DTO;

namespace IntelboardCore.DAL.Interfaces
{
    public interface ICraftableItemManager
    {
        Task<List<CraftableItemDto>> GetCraftableItemsAsync();
    }
}
