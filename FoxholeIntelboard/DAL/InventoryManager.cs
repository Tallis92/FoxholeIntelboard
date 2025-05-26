using IntelboardAPI.Models;
using IntelboardAPI.DTO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DAL
{
    public class InventoryManager
    {
        private readonly HttpClient _httpClient;
        private readonly AmmunitionManager _ammunitionManager;
        private readonly WeaponManager _weaponManager;
        private readonly MaterialManager _materialManager;

        public InventoryManager(HttpClient httpClient, AmmunitionManager ammunitionManager, WeaponManager weaponManager, MaterialManager materialManager)
        {
            _httpClient = httpClient;
            _ammunitionManager = ammunitionManager;
            _weaponManager = weaponManager;
            _materialManager = materialManager;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }
        public async Task<List<InventoryDto>> GetInventoriesAsync()
        {
            string uri = "/api/Inventory/";
            var inventories = new List<InventoryDto>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                inventories = JsonSerializer.Deserialize<List<InventoryDto>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting inventories: {response.StatusCode} {response.Content}");
            }

            return inventories;
        }

        public async Task CreateInventoryAsync(InventoryDto inventory)
        {
            string uri = "/api/Inventory/";
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(inventory, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Inventory created successfully." : $"Error creating inventory: {response.StatusCode} {response.Content}");

        }
        public async Task DeleteInventoryAsync(Guid? id)
        {
            string uri = $"/api/Inventory/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Inventory deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Error deleting inventory: {response.StatusCode} {response.Content}");
            }
        }
        public async Task<InventoryDto> GetInventoryByIdAsync(Guid? id)
        {
            string uri = $"/api/Inventory/{id}";
            InventoryDto inventory = null;
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                inventory = JsonSerializer.Deserialize<InventoryDto>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting inventory by ID: {response.StatusCode} {response.Content}");
            }
            return inventory;
        }

        public async Task EditInventoryAsync(InventoryDto inventory)
        {
            string uri = $"/api/Inventory/{inventory.InventoryId}";
            var json = JsonSerializer.Serialize(inventory);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Inventory updated successfully." : $"Error updated inventory: {response.StatusCode} {response.Content}");

        }

        // Uses switch case to determine what type of object input is to select wich manager to get the item from.
        // This avoids missmatches with Id's from different tables.
        public async Task<CraftableItem?> getInputItemAsync(CratedItemInput input)
        {
            CraftableItem? item = null;
            switch (input.Type)
            {
                case "Ammunition":
                    item = await _ammunitionManager.GetAmmunitionByIdAsync(input.Id);
                    break;
                case "Weapon":
                    item = await _weaponManager.GetWeaponByIdAsync(input.Id);
                    break;
                case "Material":
                    item = await _materialManager.GetMaterialByIdAsync(input.Id);
                    break;
                default:

                    item = null;
                    break;
            }
            return item;
        }
    }
}
