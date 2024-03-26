using Microsoft.AspNetCore.Components;
using TMA_Warehouse.Client.Pages.Items;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Pages.Requests
{
    public class OrderBase : ComponentBase
    {
        [Inject] internal OrderService OrderService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }

        internal IEnumerable<OrderDTO> Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Orders = await OrderService.GetOrders();
        }

        internal decimal GetTotalPrice(OrderDTO orderDTO)
        {
            return orderDTO.OrderedItems.Select(x => x.PriceWithoutVat).Sum();
        }
        internal void OrderDetailsButtonAction(OrderDTO order)
        {
            
        }
        internal async void RemoveOrderButtonAction(OrderDTO order)
        {
            await OrderService.RemoveOrder(order);
            Orders = await OrderService.GetOrders();
            StateHasChanged();

        }

        internal void ConfirmOrderButtonAction(OrderDTO order)
        {

        }

        internal async void SearchForItems(string str)
        {
            Orders = await OrderService.GetOrders();
            if (!string.IsNullOrEmpty(str))
            {
                Orders = Orders.Where(x => x.EmployeeName.StartsWith(str.ToLower()));
            }
            StateHasChanged();
        }
    }
}
