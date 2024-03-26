using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TMA_Warehouse.Client.Services;

namespace TMA_Warehouse.Client.Pages
{
    public class ItemFormBase : ComponentBase
    {
        [Inject] internal ItemService ItemService { get; set; }


        [Inject] internal NavigationManager NavigationManager { get; set; }

        [Inject] internal HttpClient Http { get; set; }

        internal ItemDTO ItemDTO { get; set; }


        internal bool WasItemPassedInUri;
        internal string ApplyButtonText => WasItemPassedInUri ? "Update Item" : "Add Item";

        internal FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message = "Field is required" } };
        internal FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        internal FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };

        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

            if (queryParameters.AllKeys.Contains("id") && !string.IsNullOrEmpty(queryParameters["id"]))
            {
                string itemId = queryParameters["id"];
                ItemDTO = await ItemService.GetItem(int.Parse(itemId));
                WasItemPassedInUri = ItemDTO != null ? true : false;
            }
            else
            {
                ItemDTO = new ItemDTO();
                ItemDTO.Id = await ItemService.GetBiggestItemId() + 1;   //await Http.GetFromJsonAsync<int>($"Lists/Items/GetBiggestItemId") + 1;

                WasItemPassedInUri = false;
            }
        }


        internal async Task UpdateItem()
        {
            try
            {
                HttpResponseMessage response = await ItemService.UpdateItem(ItemDTO.Id, ItemDTO);

                if (response.IsSuccessStatusCode)
                {
                    string updatedItemJson = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Item updated successfully: " + updatedItemJson);
                }
                else
                {
                    Console.WriteLine("Failed to update " + ItemDTO.Name);
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
                HttpResponseMessage response = await ItemService.AddItem(ItemDTO);

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
            NavigationManager.NavigateTo($"/Lists/Items");
        }

        internal void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"OnFinishFailed Failed:{JsonSerializer.Serialize(ItemDTO)}");
            NavigationManager.NavigateTo($"/addItem?id={ItemDTO.Id}");
        }
    }


}
