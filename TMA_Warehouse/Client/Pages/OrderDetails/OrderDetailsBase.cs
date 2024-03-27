namespace TMA_Warehouse.Client.Pages.OrderDetails
{
    using Microsoft.AspNetCore.Components;
    using System.Net.Http.Json;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using TMA_Warehouse.Client.FrontendModels;
    using TMA_Warehouse.Client.Services;
    using TMA_Warehouse.Shared.DTOs;

    public class OrderDetailsBase : ComponentBase
    {
        [Inject] internal ItemService ItemService { get; set; }
        [Inject] internal OrderService OrderService { get; set; }
        [Inject] internal NavigationManager NavigationManager { get; set; }

        internal List<OrderDTO> OrderDTO { get; set; } = new List<OrderDTO>();
        internal List<ItemOrderDetails> OrderedItems { get; set; } = new List<ItemOrderDetails>();

        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

            if (queryParameters.AllKeys.Contains("id") && !string.IsNullOrEmpty(queryParameters["id"]))
            {
                string itemId = queryParameters["id"];

                OrderDTO.Add(await OrderService.GetOrder(int.Parse(itemId)));

                if (OrderDTO.Count > 0 || OrderDTO[0] != null)
                {
                    foreach (OrderedItemDTO orderedItem in OrderDTO[0].OrderedItems)
                    {
                        ItemDTO itemDto = await ItemService.GetItem(orderedItem.ItemId);
                        OrderedItems.Add(new ItemOrderDetails
                        {
                            ItemName = itemDto.Name,
                            UnitOfMeasurement = orderedItem.UnitOfMeasurement,
                            Quantity = orderedItem.Quantity,
                            PriceWithoutVat = orderedItem.PriceWithoutVat,
                            Comment = orderedItem.Comment
                        });
                    }
                }
            }
        }
    }
}
