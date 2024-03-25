using System.Net.Http.Json;
using TMA_Warehouse.Client.Models;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Shared.DTOs;

public class ItemService
{
    private readonly HttpClient httpClient;
    private readonly ItemGroupService itemGroupService;
    private readonly UnitOfMeasurementService unitOfMeasurementService;

    public ItemService(HttpClient httpClient, ItemGroupService itemGroupService, UnitOfMeasurementService unitOfMeasurementService)
    {
        this.httpClient = httpClient;
        this.itemGroupService = itemGroupService;
        this.unitOfMeasurementService = unitOfMeasurementService;
    }

    public async Task<IEnumerable<ItemFrontendModel>> GetItems()
    {
        try
        {
            var items = await httpClient.GetFromJsonAsync<IEnumerable<ItemDTO>>("Lists/Items/GetItems");
            var itemGroups = await itemGroupService.GetItemGroups();
            var unitsOfMeasurements = await unitOfMeasurementService.GetUnitsOfMeasurements();

            var result = items.Select(async item => new ItemFrontendModel
            {
                Id = item.Id,
                Name = item.Name,
                ItemGroupId = item.ItemGroupId,
                ItemGroupName = itemGroups.First(x => x.Id == item.ItemGroupId).Name,
                UnitOfMeasurementId = item.UnitOfMeasurementId,
                UnitOfMeasurementName = unitsOfMeasurements.First(x => x.Id == item.UnitOfMeasurementId).Name,
                Quantity = item.Quantity,
                PriceWithoutVAT = item.PriceWithoutVAT,
                Status = item.Status,
                StorageLocation = item.StorageLocation,
                ContactPerson = item.ContantPerson,
                PhotoURL = item.PhotoURL
            });

            return await Task.WhenAll(result);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ItemFrontendModel> GetItem(int id)
    {
        try
        {
            var item = await httpClient.GetFromJsonAsync<ItemDTO>($"Lists/Items/GetItem/{id}");
            var itemGroups = await itemGroupService.GetItemGroups();
            var unitsOfMeasurements = await unitOfMeasurementService.GetUnitsOfMeasurements();

            return new ItemFrontendModel
            {
                Id = item.Id,
                Name = item.Name,
                ItemGroupId = item.ItemGroupId,
                ItemGroupName = itemGroups.First(x => x.Id == item.ItemGroupId).Name,
                UnitOfMeasurementId = item.UnitOfMeasurementId,
                UnitOfMeasurementName = unitsOfMeasurements.First(x => x.Id == item.UnitOfMeasurementId).Name,
                Quantity = item.Quantity,
                PriceWithoutVAT = item.PriceWithoutVAT,
                Status = item.Status,
                StorageLocation = item.StorageLocation,
                ContactPerson = item.ContantPerson,
                PhotoURL = item.PhotoURL
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> UpdateItem(int id, ItemFrontendModel item)
    {
        return await httpClient.PutAsJsonAsync($"Lists/Items/UpdateItem/{id}", item.ConvertToItemDto());
    }

    public async Task<HttpResponseMessage> AddItem(ItemFrontendModel item)
    {
        return await httpClient.PostAsJsonAsync<ItemDTO>($"Lists/Items/AddItem", item.ConvertToItemDto());
    }
}