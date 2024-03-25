using System.Net.Http.Json;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Services
{
    public class ItemGroupService
    {
        private readonly HttpClient httpClient;

        public ItemGroupService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ItemGroup>> GetItemGroups()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<ItemGroup>>($"api/ItemGroup/GetItemGroups");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ItemGroup> GetItemGroup(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ItemGroup>($"api/ItemGroup/GetItemGroup/{id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
