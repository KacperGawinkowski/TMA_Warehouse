using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Server.Repositories;
using TMA_Warehouse.Shared.Models;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Client.Pages;

namespace TMA_Warehouse.Server.Controllers
{
    [ApiController]
    [Route("Lists/Items")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ItemRepository _itemRepository;
        private readonly ItemGroupRepository _itemGroupRepository;
        private readonly UnitOfMeasureRepository _unitOfMeasureRepository;

        public ItemController(ILogger<ItemController> logger, ItemRepository itemRepository, UnitOfMeasureRepository unitOfMeasureRepository, ItemGroupRepository itemGroupRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
            _itemGroupRepository = itemGroupRepository;
            _unitOfMeasureRepository = unitOfMeasureRepository;
        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            try
            {
                IEnumerable<Item> items = await _itemRepository.GetItems();
                IEnumerable<ItemGroup> itemGroups = await _itemGroupRepository.GetItemGroups();
                IEnumerable<UnitOfMeasurement> unitsOfMeasure = await _unitOfMeasureRepository.GetUnitsOfMeasurements();

                if (items == null || itemGroups == null || unitsOfMeasure == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemDtos = items.ConvertToDto(itemGroups, unitsOfMeasure);
                    return Ok(itemDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            try
            {
                Item item = await _itemRepository.GetItem(id);
                IEnumerable<ItemGroup> itemGroups = await _itemGroupRepository.GetItemGroups();
                IEnumerable<UnitOfMeasurement> unitsOfMeasure = await _unitOfMeasureRepository.GetUnitsOfMeasurements();

                if (item == null || itemGroups == null || unitsOfMeasure == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemDto = item.ConvertToDto(itemGroups, unitsOfMeasure);
                    return Ok(itemDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemDTO itemDto)
        {
            if (itemDto == null) return new StatusCodeResult(400);

            _itemRepository.AddItem(new Item(itemDto));
            return Ok();
        }

        [HttpPut]
        [Route("UpdateItem/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] ItemDTO itemDto)
        {
            try
            {
                await _itemRepository.UpdateItem(id, new Item(itemDto));
                var updatedItem = await GetItem(id);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }
        }


        [HttpDelete]
        [Route("RemoveItem/{id}")]
        public async Task<IActionResult> RemovItem(int id)
        {
            try
            {
                await _itemRepository.RemoveItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpGet]
        [Route("GetBiggestItemId")]
        public async Task<ActionResult<int>> GetBiggestItemId()
        {
            try
            {
                IEnumerable<Item> items = await _itemRepository.GetItems();

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
