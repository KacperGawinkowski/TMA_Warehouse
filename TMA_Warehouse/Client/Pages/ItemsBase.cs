using Microsoft.AspNetCore.Components;
using System.Reflection;
using TMA_Warehouse.Shared.DTOs;
using System.Net.Http.Json;

namespace TMA_Warehouse.Client.Pages
{
    public class ItemsBase:ComponentBase
    {
        [Inject]
        internal ItemService ItemService { get; set; }

        [Inject]
        internal NavigationManager NavigationManager { get; set; }

        [Inject]
        internal HttpClient Http{ get; set; }

        internal IEnumerable<ItemDTO> Items { get; set; }
        internal PropertyInfo[] Properties = typeof(ItemDTO).GetProperties();

        internal PropertyInfo CurrentSortProperty;
        internal bool ascending = false;

        internal string searchNameText = "";

        protected override async Task OnInitializedAsync()
        {
            Items = await ItemService.GetItems();
        }

        internal void Sort(PropertyInfo propertyInfo)
        {
            if (CurrentSortProperty == propertyInfo)
            {
                ascending = !ascending;
            }
            else
            {
                CurrentSortProperty = propertyInfo;
                ascending = false;
            }

            if (ascending)
            {
                Items = Items.OrderBy(item => CurrentSortProperty.GetValue(item)).ToArray();
            }
            else
            {
                Items = Items.OrderByDescending(item => CurrentSortProperty.GetValue(item)).ToArray();
            }
        }

        internal async void UpdateItem(ItemDTO item)
        {
            NavigationManager.NavigateTo($"/addItem?id={item.Id}");
        }

        internal async void RemoveItem(ItemDTO item)
        {
            await Http.DeleteAsync($"Lists/Items/RemoveItem/{item.Id}");
            Items = await Http.GetFromJsonAsync<ItemDTO[]>("Lists/Items/GetItems");
            StateHasChanged();
        }

        internal async void AddItem()
        {
            NavigationManager.NavigateTo("additem");
        }

        internal async void SearchForItems()
        {
            Items = await ItemService.GetItems();
            if (!string.IsNullOrEmpty(searchNameText))
            {
                Items = Items.Where(x => x.Name.ToLower().StartsWith(searchNameText.ToLower()));
            }
            StateHasChanged();
        }
    }
}
