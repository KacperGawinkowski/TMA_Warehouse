using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using WarehouseAPI.Models;

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
            return Ok(await context.Items.Select(x => new ItemDTO(x)).ToListAsync());
        }

        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            var item = await context.Items.Where(x => x.Id == id).Select(x => new ItemDTO(x)).FirstOrDefaultAsync();
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
            try
            {
                Item newItem = new Item(itemDto);
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
                Item existingItem = await context.Items.FindAsync(id);

                if (existingItem == null)
                {
                    return NotFound(id);
                }

                existingItem.Id = itemDto.Id;
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

        [HttpGet]
        [Route("GetBiggestItemId")]
        public async Task<ActionResult<int>> GetBiggestItemId()
        {
            try
            {
                IEnumerable<Item> items = await context.Items.ToListAsync();

                if (items == null)
                {
                    return NotFound();
                }
                else
                {
                    int maxId = items.Max(x => x.Id);
                    return Ok(maxId);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
