using System.Net.Http.Json;
using TMA_Warehouse.Shared.DTOs;

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
            var items = await httpClient.GetFromJsonAsync<IEnumerable<ItemDTO>>("Lists/Items/GetItems");
            return items;
        }
        catch (Exception)
        {
            throw;
        }
    }
}