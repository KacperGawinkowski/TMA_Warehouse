using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Server.Repositories;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly ILogger<UnitOfMeasurementController> _logger;
        private readonly UnitOfMeasureRepository _repository;

        public UnitOfMeasurementController(ILogger<UnitOfMeasurementController> logger, UnitOfMeasureRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("GetUnitsOfMeasurements")]
        public async Task<IEnumerable<UnitOfMeasurement>> GetUnitsOfMeasurements()
        {
            return await _repository.GetUnitsOfMeasurements();
        }

        [HttpGet]
        [Route("GetUnitOfMeasurement/{id}")]
        public async Task<UnitOfMeasurement> GetUnitOfMeasurement(int id)
        {
            return await _repository.GetUnitOfMeasurement(id);
        }
    }
}
