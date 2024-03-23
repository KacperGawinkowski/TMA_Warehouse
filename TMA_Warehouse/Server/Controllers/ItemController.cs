using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Server.Repositories;
using TMA_Warehouse.Shared.Models;
using TMA_Warehouse.Shared.DTOs;

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
        public async Task<IEnumerable<ItemDTO>> GetItems()
        {
            IEnumerable<Item> items = await _itemRepository.GetItems();
            IEnumerable<ItemGroup> itemGroups = await _itemGroupRepository.GetItemGroups();
            IEnumerable<UnitOfMeasurement> unitsOfMeasure = await _unitOfMeasureRepository.GetUnitsOfMeasurements();

            return items.ConvertToDto(itemGroups, unitsOfMeasure);
        }

        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<ItemDTO> GetItem(int id)
        {
            //IEnumerable<Item> items = await _itemRepository.GetItems();
            //IEnumerable<ItemGroup> itemGroups = await _itemGroupRepository.GetItemGroups();
            //IEnumerable<UnitOfMeasurement> unitsOfMeasure = await _unitOfMeasureRepository.GetUnitsOfMeasurements();

            //return items.ConvertToDto(itemGroups, unitsOfMeasure);

            return null;
        }
    }
}
