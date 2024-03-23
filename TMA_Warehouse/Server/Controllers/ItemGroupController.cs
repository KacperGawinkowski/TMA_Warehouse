using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Server.Repositories;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemGroupController : ControllerBase
    {
        private readonly ILogger<ItemGroupController> _logger;
        private readonly ItemGroupRepository _repository;

        public ItemGroupController(ILogger<ItemGroupController> logger, ItemGroupRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("GetItemGroups")]
        public async Task<IEnumerable<ItemGroup>> GetItemGroups()
        {
            return await _repository.GetItemGroups();
        }

        [HttpGet]
        [Route("GetItemGroup/{id}")]
        public async Task<ItemGroup> GetItemGroup(int id)
        {
            return await _repository.GetItemGroup(id);
        }
    }
}
