using Microsoft.AspNetCore.Components;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Shared.DTOs;

namespace TMA_Warehouse.Client.Pages.Items
{
    public class ItemsBase : ComponentBase
    {
        [Inject] internal ItemService ItemService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }
        [Inject] internal UserService UserService { get; set; }

        internal IEnumerable<ItemDTO> Items { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (UserService.LoggedUser.Role == Role.Guest)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            Items = await ItemService.GetItems();
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

        internal void OrderItemButtonAction()
        {
            NavigationManager.NavigateTo($"/orderItem");
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
