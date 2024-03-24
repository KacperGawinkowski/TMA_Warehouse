using Microsoft.AspNetCore.Components;
using System.Reflection;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Pages
{
    public class ItemFormBase : ComponentBase
    {
        [Inject]
        internal ItemService ItemService { get; set; }

        [Inject]
        internal NavigationManager NavigationManager { get; set; }

        [Inject]
        internal HttpClient Http { get; set; }

        internal ItemDTO Item { get; set; }
        internal PropertyInfo[] Properties = typeof(ItemDTO).GetProperties();
        internal ItemGroup[] ItemGroups;

        protected override async Task OnInitializedAsync()
        {
            
            //check if ItemDTO was passed in URI

        }


        internal async void UpdateItem(ItemDTO item)
        {
            throw new NotImplementedException();
        }

        internal async void AddItem()
        {
            throw new NotImplementedException();
        }

        internal async void HandleValidSubmit()
        {
            
        }
    }
}
