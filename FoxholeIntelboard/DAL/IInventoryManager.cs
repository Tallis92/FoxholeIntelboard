using IntelboardAPI.DTO;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.DAL
{
    public interface IInventoryManager
    {
        Task<List<InventoryDto>> GetInventoriesAsync();
        Task CreateInventoryAsync(InventoryDto inventory);
        Task DeleteInventoryAsync(Guid? id);
        Task<InventoryDto> GetInventoryByIdAsync(Guid? id);
        Task EditInventoryAsync(InventoryDto inventory);
        Task<CraftableItem?> getInputItemAsync(CratedItemInput input);
    }
}
