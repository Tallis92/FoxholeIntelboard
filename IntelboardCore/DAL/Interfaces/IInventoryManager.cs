using IntelboardCore.DTO;
using IntelboardCore.Models;

namespace IntelboardCore.DAL.Interfaces
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
