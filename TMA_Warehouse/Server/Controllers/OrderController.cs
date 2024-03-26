using AntDesign;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using TMA_Warehouse.Client.Pages.Requests;
using TMA_Warehouse.Client.Services;
using TMA_Warehouse.Server;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;
namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly WarehouseContext context;
        private readonly ItemController itemController;

        public OrderController(WarehouseContext context, ItemController itemController)
        {
            this.context = context;
            this.itemController = itemController;
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            List<Order> orders = await context.Orders.Include(x => x.OrderedItems).ToListAsync();

            var orderDTOs = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                EmployeeName = o.EmployeeName,
                Comment = o.Comment,
                Status = o.Status,
                OrderedItems = o.OrderedItems.Select(orderedItem => new OrderedItemDTO
                {
                    ItemId = orderedItem.ItemId,
                    OrderId = orderedItem.OrderId,
                    UnitOfMeasurement = orderedItem.UnitOfMeasurement,
                    Quantity = orderedItem.Quantity,
                    PriceWithoutVat = orderedItem.PriceWithoutVat,
                    Comment = orderedItem.Comment
                }).ToList()
            }).ToList();

            return orderDTOs;
        }

        [HttpGet]
        [Route("GetOrder/{id}")]
        public async Task<ActionResult<ItemDTO>> GetOrder(int id)
        {
            Order order = await context.Orders.Include(x => x.OrderedItems).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                EmployeeName = order.EmployeeName,
                Comment = order.Comment,
                Status = order.Status,
                OrderedItems = order.OrderedItems.Select(orderedItem => new OrderedItemDTO
                {
                    ItemId = orderedItem.ItemId,
                    OrderId = orderedItem.OrderId,
                    UnitOfMeasurement = orderedItem.UnitOfMeasurement,
                    Quantity = orderedItem.Quantity,
                    PriceWithoutVat = orderedItem.PriceWithoutVat,
                    Comment = orderedItem.Comment
                }).ToList()
            };

            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDto)
        {
            try
            {
                Order newOrder = new Order
                {
                    EmployeeName = orderDto.EmployeeName,
                    Comment = orderDto.Comment,
                    Status = orderDto.Status,
                };
                context.Orders.Add(newOrder);


                foreach (OrderedItemDTO item in orderDto.OrderedItems)
                {
                    OrderedItem newOrderItem = new OrderedItem
                    {
                        ItemId = item.ItemId,
                        Order = newOrder,
                        UnitOfMeasurement = item.UnitOfMeasurement,
                        Quantity = item.Quantity,
                        PriceWithoutVat = item.PriceWithoutVat,
                        Comment = item.Comment
                    };

                    context.OrderedItems.Add(newOrderItem);
                }

                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the item: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("RemoveOrder/{id}")]
        public async Task<ActionResult> RemoveOrder(int id)
        {
            Order orderActionResult = await context.Orders.Where(x => x.Id == id).Include(x => x.OrderedItems).FirstOrDefaultAsync();
            if (orderActionResult == null)
            {
                return NotFound();
            }

            foreach (OrderedItem item in orderActionResult.OrderedItems)
            {
                context.OrderedItems.Remove(item);
            }
            await context.SaveChangesAsync();


            context.Orders.Remove(orderActionResult);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("ChangeOrderStatus/{id}")]
        public async Task<ActionResult> ChangeOrderStatus(int id, [FromBody] string status)
        {
            Order orderToChange = await context.Orders.Where(x => x.Id == id).Include(x => x.OrderedItems).FirstOrDefaultAsync();

            orderToChange.Status = status;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("ConfirmOrder/{id}")]
        public async Task<ActionResult> ConfirmOrder(int id, [FromBody] OrderDTO orderDto)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (OrderedItemDTO orderedItem in orderDto.OrderedItems)
                    {
                        await itemController.UpdateAmount(orderedItem.ItemId, orderedItem.Quantity);
                    }

                    await ChangeOrderStatus(id, "Aproved");

                    await context.SaveChangesAsync();

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return StatusCode(500, $"An error occurred while adding the item: {ex.Message}");
                }
            }
        }
    }
}
