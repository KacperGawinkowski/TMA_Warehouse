using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        internal string ApplyButtonText => WasItemPassedInUri ? "Update Item" : "Add Item";
        internal ItemDTO Item { get; set; }
        internal IEnumerable<ItemGroup> ItemGroups;
        internal IEnumerable<UnitOfMeasurement> UnitsOfMeasurements;

        internal ItemFormModel ItemFormModel;

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
                Item.Id = await Http.GetFromJsonAsync<int>($"Lists/Items/GetBiggestItemId") + 1;
                Console.WriteLine("Biggest found Id = " + Item.Id);
                WasItemPassedInUri = false;
            }

            ItemGroups = await Http.GetFromJsonAsync<ItemGroup[]>($"api/ItemGroup/GetItemGroups");
            UnitsOfMeasurements = await Http.GetFromJsonAsync<UnitOfMeasurement[]>($"api/UnitOfMeasurement/GetUnitsOfMeasurements");

            ItemFormModel = new ItemFormModel(Item);
        }


        internal async Task UpdateItem()
        {
            try
            {
                HttpResponseMessage response = await Http.PutAsJsonAsync($"Lists/Items/UpdateItem/{Item.Id}", Item);

                if (response.IsSuccessStatusCode)
                {
                    string updatedItemJson = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Item updated successfully: " + updatedItemJson);
                }
                else
                {
                    Console.WriteLine("Failed to update " + Item.Name);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Update Item exception");
            }
        }

        internal async Task AddItem()
        {
            try
            {
                HttpResponseMessage response = await Http.PostAsJsonAsync<ItemDTO>($"Lists/Items/AddItem", Item);

                if (response.IsSuccessStatusCode)
                {
                    string updatedItem = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Item added succesfully" + updatedItem);
                }
                else
                {
                    Console.WriteLine("Item added unsuccesfully");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Add Item exception");
            }
        }

        internal async void OnFinish(EditContext editContext)
        {
            Console.WriteLine("OnFinish");
            if (WasItemPassedInUri)
            {
                await UpdateItem();
            }
            else
            {
                await AddItem();
            }
        }

        internal void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine("OnFinishFailed");
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(Item)}");
        }

        internal void UpdateItemGroupName()
        {
            Item.ItemGroupName = ItemGroups.First(x => x.Id == Item.ItemGroupId).Name;
        }

        internal void UpdateUnitOfMeasurementName()
        {
            Item.UnitOfMeasurementName = UnitsOfMeasurements.First(x => x.Id == Item.UnitOfMeasurementId).Name;
        }
    }

    public class ItemFormModel
    {
        public string Size { get; set; } = AntSizeLDSType.Small;
        public string ItemName { get; set; } = "";
        public int ItemGroupId { get; set; }
        public string ItemGroupName { get; set; } = "";
        public int UnitOfMeasurementId { get; set; }
        public string UnitOfMeasurementName { get; set; } = "";
        public double Quantity { get; set; } = 1;
        public decimal PriceWithoutVAT { get; set; } = 0m;
        public string Status { get; set; } = "";
        public string StorageLocation { get; set; } = "";
        public string ContactPerson { get; set; } = "";


        public ItemFormModel(ItemDTO item)
        {
            ItemName = item.Name;
            ItemGroupId = item.ItemGroupId;
            ItemGroupName = item.ItemGroupName;
            UnitOfMeasurementId = item.UnitOfMeasurementId;
            UnitOfMeasurementName = item.UnitOfMeasurementName;
            Quantity = item.Quantity;
            PriceWithoutVAT = item.PriceWithoutVAT;
            Status = item.Status;
            StorageLocation = item.StorageLocation;
            ContactPerson = item.ContantPerson;
        }
    }
}
