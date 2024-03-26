using System.Net.Http.Json;
using TMA_Warehouse.Shared.DTOs;

namespace TMA_Warehouse.Client.Services
{
    public class ItemService
    {
        private readonly HttpClient httpClient;
        public ItemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ItemDTO>> GetItems()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<IEnumerable<ItemDTO>>("api/Item/GetItems");

                return res;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<ItemDTO> GetItem(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ItemDTO>($"api/Item/GetItem/{id}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateItem(int id, ItemDTO itemDto)
        {
            try
            {
                return await httpClient.PutAsJsonAsync($"api/Item/UpdateItem/{id}", itemDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> AddItem(ItemDTO itemDto)
        {
            try
            {
                return await httpClient.PostAsJsonAsync<ItemDTO>($"api/Item/AddItem", itemDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> RemoveItem(ItemDTO itemDto)
        {
            return await httpClient.DeleteAsync($"api/Item/RemoveItem/{itemDto.Id}");
        }

        public async Task<HttpResponseMessage> UpdateItemAmount(int id, float amount)
        {
            ItemDTO item = await GetItem(id);

            return await httpClient.PutAsJsonAsync<float>($"api/Item/UpdateAmount/{id}", item.Quantity - amount);
        }
    }
}
