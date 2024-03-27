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
        [Inject] internal ItemService OrderService { get; set; }
        [Inject] internal ItemService ItemService { get; set; }
        [Inject] internal IMessageService MessageService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }
        [Inject] internal HttpClient Http { get; set; }

        internal OrderDTO OrderDTO { get; set; }
        internal ItemOrderModel ItemToAdd;
        internal List<OrderedItemDTO> OrderedItemDTOs { get; set; }
        internal IEnumerable<ItemDTO> Items { get; set; }

        internal string SelectedItemIdAsString { get; set; }

        internal FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message = "Field is required" } };
        internal FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        internal FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };

        protected override async Task OnInitializedAsync()
        {
            OrderDTO = new OrderDTO();
            OrderedItemDTOs = new List<OrderedItemDTO>();
            ItemToAdd = new ItemOrderModel();

            Items = await ItemService.GetItems();
        }

        internal async void OnFinishOrder(EditContext context)
        {
            Console.WriteLine("OnFinish");
            NavigationManager.NavigateTo($"/Lists/Items");
        }

        internal async void OnFinishAddingItemToOrder(ItemOrderModel context)
        {
            OrderedItemDTO orderedItemDTO = new OrderedItemDTO
            {
                ItemId = Items.Where(x => x.Name == context.ItemName).First().Id,
                UnitOfMeasurement = context.UnitOfMeasurement,
                Quantity = context.Quantity,
                PriceWithoutVat = context.PriceWithoutVat,
                Comment = context.Comment
            };

            OrderedItemDTOs.Add(orderedItemDTO);

            if(OrderedItemDTOs.Count > 1)
            {
                await MessageService.Success("Request created");
            }
            else
            {
                await MessageService.Success("Request updated");
            }

            //update orderItemForm itemslist

            ItemToAdd = new ItemOrderModel();
        }

    }

    public class KeyStringSelect : Select<int, string> { }

    public class KeyStringSelectSelectOption : SelectOption<int, string> { }
}
