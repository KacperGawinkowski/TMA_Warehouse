using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
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

        internal bool WasItemPassedInUri;
        internal ItemDTO Item { get; set; }
        internal ItemGroup[] ItemGroups;
        internal UnitOfMeasurement[] UnitOfMeasurements;

        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

            if (queryParameters.AllKeys.Contains("id") && !string.IsNullOrEmpty(queryParameters["id"]))
            {
                string itemId = queryParameters["id"];
                Item = await Http.GetFromJsonAsync<ItemDTO>($"Lists/Items/GetItem/{itemId}");
                WasItemPassedInUri = Item != null ? true : false;
            }
            else
            {
                Item = new ItemDTO();
                WasItemPassedInUri = false;
            }
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
