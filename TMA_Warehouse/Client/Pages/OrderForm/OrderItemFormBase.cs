using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TMA_Warehouse.Client.FrontendModels;
using TMA_Warehouse.Client.Pages.OrderDetails;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Shared.DTOs;

namespace TMA_Warehouse.Client.Pages
{
    public class OrderItemFormBase : ComponentBase
    {
        [Inject] internal OrderService OrderService { get; set; }
        [Inject] internal ItemService ItemService { get; set; }
        [Inject] internal MessageService MessageService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }
        [Inject] internal HttpClient Http { get; set; }

        internal OrderDTO OrderDTO { get; set; }
        internal ItemOrderModel ItemToAdd;
        internal IEnumerable<ItemOrderModel> OrderedItems { get; set; }
        internal IEnumerable<ItemDTO> Items { get; set; }

        internal bool showSuccess = false;
        internal string popupTitle = "";
        internal string popupMessage = "";

        internal FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message = "Field is required" } };
        internal FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        internal FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };

        protected override async Task OnInitializedAsync()
        {
            OrderDTO = new OrderDTO();
            OrderedItems = new List<ItemOrderModel>();
            ItemToAdd = new ItemOrderModel();

            Items = await ItemService.GetItems();
        }

        internal async void OnFinishOrder(EditContext context)
        {
            try
            {
                List<OrderedItemDTO> orderedItems = new List<OrderedItemDTO>();
                foreach (var item in OrderedItems)
                {
                    orderedItems.Add(new OrderedItemDTO
                    {
                        ItemId = Items.First(x => x.Name == item.ItemName).Id,
                        UnitOfMeasurement = item.UnitOfMeasurement,
                        Quantity = item.Quantity,
                        PriceWithoutVat = item.PriceWithoutVat,
                        Comment = item.Comment,
                    });
                }
                OrderDTO.OrderedItems = orderedItems;

                HttpResponseMessage res = await OrderService.AddOrder(OrderDTO);
                if(res.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo($"/Lists/Items");
                }
                else
                {
                    await ShowPopupMessage("Failed", "Request couldnt be created", 3f);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        internal async void OnFinishAddingItemToOrder(ItemOrderModel context)
        {
            List<ItemOrderModel> orderedItemsList = OrderedItems.ToList();

            orderedItemsList.Add(context);
            OrderedItems = orderedItemsList;

            if (orderedItemsList.Count > 1)
            {
                await ShowPopupMessage("Success", "Request created", 3f);
            }                                                          
            else                                                       
            {                                                          
                await ShowPopupMessage("Success", "Request updated", 3f);
            }

            ItemToAdd = new ItemOrderModel();
            StateHasChanged();
        }

        internal void CancelOrder()
        {
            NavigationManager.NavigateTo($"/Lists/Items");
        }

        public async Task ShowPopupMessage(string messageTitle, string message, float popupTimeInSeconds)
        {
            showSuccess = true;
            popupTitle = messageTitle;
            popupMessage = message;
            StateHasChanged();

            await Task.Delay((int)(popupTimeInSeconds * 1000));
            showSuccess = false;
            StateHasChanged();
        }

    }
}
