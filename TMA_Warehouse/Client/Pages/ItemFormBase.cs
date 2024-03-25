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
using TMA_Warehouse.Client.Models;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Pages
{
    public class ItemFormBase : ComponentBase
    {
        [Inject]
        internal ItemService ItemService { get; set; }
        [Inject]
        internal ItemGroupService ItemGroupService { get; set; }
        [Inject]
        internal UnitOfMeasurementService UnitOfMeasurementService { get; set; }

        [Inject]
        internal NavigationManager NavigationManager { get; set; }

        [Inject]
        internal HttpClient Http { get; set; }

        internal bool WasItemPassedInUri;
        internal string ApplyButtonText => WasItemPassedInUri ? "Update Item" : "Add Item";

        internal IEnumerable<ItemGroup> ItemGroups;
        internal IEnumerable<UnitOfMeasurement> UnitsOfMeasurements;

        internal ItemFrontendModel ItemFrontendModel { get; set; }

        internal FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message="Field is required" } };
        internal FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        internal FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m  } };

        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

            ItemGroups = await ItemGroupService.GetItemGroups();
            UnitsOfMeasurements = await UnitOfMeasurementService.GetUnitsOfMeasurements();

            if (queryParameters.AllKeys.Contains("id") && !string.IsNullOrEmpty(queryParameters["id"]))
            {
                string itemId = queryParameters["id"];
                ItemFrontendModel = await ItemService.GetItem(int.Parse(itemId));
                WasItemPassedInUri = ItemFrontendModel != null ? true : false;
            }
            else
            {
                ItemFrontendModel = new ItemFrontendModel();
                ItemFrontendModel.Id = await Http.GetFromJsonAsync<int>($"Lists/Items/GetBiggestItemId") + 1;
                WasItemPassedInUri = false;
            }
        }


        internal async Task UpdateItem()
        {
            try
            {
                HttpResponseMessage response = await ItemService.UpdateItem(ItemFrontendModel.Id, ItemFrontendModel); 

                if (response.IsSuccessStatusCode)
                {
                    string updatedItemJson = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Item updated successfully: " + updatedItemJson);
                }
                else
                {
                    Console.WriteLine("Failed to update " + ItemFrontendModel.Name);
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
                HttpResponseMessage response = await ItemService.AddItem(ItemFrontendModel);

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
            NavigationManager.NavigateTo($"/items");
        }

        internal void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"OnFinishFailed Failed:{JsonSerializer.Serialize(ItemFrontendModel)}");
            NavigationManager.NavigateTo($"/addItem?id={ItemFrontendModel.Id}");
        }

        internal void UpdateItemGroupName()
        {
            ItemFrontendModel.ItemGroupName = ItemGroups.First(x => x.Id == ItemFrontendModel.ItemGroupId).Name;
        }

        internal void UpdateUnitOfMeasurementName()
        {
            ItemFrontendModel.UnitOfMeasurementName = UnitsOfMeasurements.First(x => x.Id == ItemFrontendModel.UnitOfMeasurementId).Name;
        }
    }


}
