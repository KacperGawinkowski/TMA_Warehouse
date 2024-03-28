using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMA_Warehouse.Server;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly WarehouseContext context;

        public ItemController(WarehouseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok(await context.Items.Include(x => x.OrderedItems).Select(x => new ItemDTO{
            Id = x.Id,
            Name = x.Name,
            ItemGroup = x.ItemGroup,
            UnitOfMeasurement = x.UnitOfMeasurement,
            Quantity = x.Quantity,
            PriceWithoutVat = x.PriceWithoutVat,
            Status = x.Status,
            StorageLocation = x.StorageLocation,
            ContactPerson = x.ContactPerson,
            PhotoUrl = x.PhotoUrl,
            }).ToListAsync());
        }

        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            var item = await context.Items.Include(x => x.OrderedItems).Where(x => x.Id == id).Select(x => new ItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                ItemGroup = x.ItemGroup,
                UnitOfMeasurement = x.UnitOfMeasurement,
                Quantity = x.Quantity,
                PriceWithoutVat = x.PriceWithoutVat,
                Status = x.Status,
                StorageLocation = x.StorageLocation,
                ContactPerson = x.ContactPerson,
                PhotoUrl = x.PhotoUrl,
            }).FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<ActionResult> AddItem([FromBody] ItemDTO itemDto)
        {
            Console.WriteLine($"UnitOfMeasurement: {itemDto.UnitOfMeasurement} {itemDto.UnitOfMeasurement.GetType()} {itemDto.UnitOfMeasurement.GetTypeCode()}");

            try
            {
                Item newItem = new Item{
                    Name = itemDto.Name,
                    ItemGroup = itemDto.ItemGroup,
                    UnitOfMeasurement = itemDto.UnitOfMeasurement,
                    Quantity = itemDto.Quantity,
                    PriceWithoutVat = itemDto.PriceWithoutVat,
                    Status = itemDto.Status,
                    StorageLocation = itemDto.StorageLocation,
                    ContactPerson = itemDto.ContactPerson,
                    PhotoUrl = itemDto.PhotoUrl,
                };

                context.Items.Add(newItem);

                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the item: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateItem/{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromBody] ItemDTO itemDto)
        {
            try
            {
                Item existingItem = await context.Items.Where(x => x.Id == id).Include(x => x.OrderedItems).FirstOrDefaultAsync();

                if (existingItem == null)
                {
                    return NotFound(id);
                }

                existingItem.Name = itemDto.Name;
                existingItem.ItemGroup = itemDto.ItemGroup;
                existingItem.UnitOfMeasurement = itemDto.UnitOfMeasurement;
                existingItem.Quantity = itemDto.Quantity;
                existingItem.PriceWithoutVat = itemDto.PriceWithoutVat;
                existingItem.Status = itemDto.Status;
                existingItem.StorageLocation = itemDto.StorageLocation;
                existingItem.ContactPerson = itemDto.ContactPerson;
                existingItem.PhotoUrl = itemDto.PhotoUrl;

                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the item: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("RemoveItem/{id}")]
        public async Task<ActionResult> RemoveItem(int id)
        {
            Item itemActionResult = await context.Items.Where(x => x.Id == id).Include(x => x.OrderedItems).FirstOrDefaultAsync();
            if (itemActionResult == null)
            {
                return NotFound();
            }

            context.Items.Remove(itemActionResult);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateAmount/{id}")]
        public async Task<ActionResult> UpdateAmount(int id, [FromBody] float amount)
        {
            Item itemActionResult = await context.Items.Where(x => x.Id == id).Include(x => x.OrderedItems).FirstOrDefaultAsync();
            if (itemActionResult == null)
            {
                return NotFound();
            }

            itemActionResult.Quantity = amount;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
