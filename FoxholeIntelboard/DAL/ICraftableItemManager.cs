using IntelboardAPI.Models;
using IntelboardAPI.DTO;

namespace FoxholeIntelboard.DAL
{
    public interface ICraftableItemManager
    {
        Task<List<CraftableItemDto>> GetCraftableItemsAsync();
    }
}
