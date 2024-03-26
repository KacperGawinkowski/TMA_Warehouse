using AntDesign;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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

        public OrderController(WarehouseContext context)
        {
            this.context = context;
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
                    Comment = orderDto.Comment
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

        //[HttpPut]
        //[Route("UpdateItem/{id}")]
        //public async Task<ActionResult> UpdateItem(int id, [FromBody] ItemDTO itemDto)
        //{
        //    try
        //    {
        //        Item existingItem = await context.Items.FindAsync(id);

        //        if (existingItem == null)
        //        {
        //            return NotFound(id);
        //        }

        //        existingItem.Id = itemDto.Id;
        //        existingItem.Name = itemDto.Name;
        //        existingItem.ItemGroup = itemDto.ItemGroup;
        //        existingItem.UnitOfMeasurement = itemDto.UnitOfMeasurement;
        //        existingItem.Quantity = itemDto.Quantity;
        //        existingItem.PriceWithoutVat = itemDto.PriceWithoutVat;
        //        existingItem.Status = itemDto.Status;
        //        existingItem.StorageLocation = itemDto.StorageLocation;
        //        existingItem.ContactPerson = itemDto.ContactPerson;
        //        existingItem.PhotoUrl = itemDto.PhotoUrl;

        //        await context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while updating the item: {ex.Message}");
        //    }
        //}


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

        //[HttpGet]
        //[Route("GetBiggestItemId")]
        //public async Task<ActionResult<int>> GetBiggestItemId()
        //{
        //    try
        //    {
        //        IEnumerable<Item> items = await context.Items.ToListAsync();

        //        if (items == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            int maxId = items.Max(x => x.Id);
        //            return Ok(maxId);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        //    }
        //}
    }
}
