using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using TMA_Warehouse.Client.Services;

namespace WarehouseApp.Pages
{
    public class ItemsBase : ComponentBase
    {
        [Inject] internal ItemService ItemService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }

        internal IEnumerable<ItemDTO> Items { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Items = await ItemService.GetItems();
            Console.WriteLine("items Count = " + Items.Count());
        }

        internal void UpdateItemButtonAction(ItemDTO item)
        {
            NavigationManager.NavigateTo($"/addItem?id={item.Id}");
        }
        internal async void RemoveItemButtonAction(ItemDTO item)
        {
            await ItemService.RemoveItem(item);
            Items = await ItemService.GetItems();
            StateHasChanged();
        }

        internal void AddItemButtonAction()
        {
            NavigationManager.NavigateTo("additem");
        }

        internal void OrderItemButtonAction(ItemDTO item)
        {
            NavigationManager.NavigateTo($"/orderItem?id={item.Id}");
        }

        internal async void SearchForItems(string str)
        {
            Items = await ItemService.GetItems();
            if (!string.IsNullOrEmpty(str))
            {
                Items = Items.Where(x => x.Name.ToLower().StartsWith(str.ToLower()));
            }
            StateHasChanged();
        }
    }
}
