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
        internal List<ItemOrderDetails> ItemOrderDetails { get; set; }

        internal FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message = "Field is required" } };
        internal FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        internal FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };

        protected override async Task OnInitializedAsync()
        {
            OrderDTO = new OrderDTO();
            ItemOrderDetails = new List<ItemOrderDetails>();
        }

        internal async void OnFinish(EditContext editContext)
        {
            Console.WriteLine("OnFinish");
            //reload Items (the amount might have changed ez)
            NavigationManager.NavigateTo($"/Lists/Items");
        }
    }


}
