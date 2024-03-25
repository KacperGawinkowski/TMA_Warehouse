//using Microsoft.AspNetCore.Mvc;
//using TMA_Warehouse.Server.Repositories;
//using TMA_Warehouse.Shared.Models;
//using TMA_Warehouse.Shared.DTOs;

//namespace TMA_Warehouse.Server.Controllers
//{
//    [ApiController]
//    [Route("Lists/Orders")]
//    public class RequestController : ControllerBase
//    {
//        private readonly ILogger<RequestController> _logger;
//        private readonly RequestRepository _requestRepository;
//        //private readonly RequestRowRepository _requestRowRepository;
//        private readonly ItemRepository _itemRepository;
//        private readonly UnitOfMeasureRepository _unitOfMeasureRepository;

//        public RequestController(ILogger<RequestController> logger, RequestRepository requestRepository, /*RequestRowRepository requestRowRepository,*/ ItemRepository itemRepository, UnitOfMeasureRepository unitOfMeasureRepository)
//        {
//            _logger = logger;
//            _requestRepository = requestRepository;
//            //_requestRowRepository = requestRowRepository;
//            _itemRepository = itemRepository;
//            _unitOfMeasureRepository = unitOfMeasureRepository;
//        }

//        [HttpGet]
//        [Route("GetRequests")]
//        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequests()
//        {
//            try
//            {
//                IEnumerable<Request> requests = await _requestRepository.GetRequests();
//                //IEnumerable<RequestRow> requestRows = await _requestRowRepository.GetRequestRows();
//                IEnumerable<Item> items = await _itemRepository.GetItems();
//                IEnumerable<UnitOfMeasurement> unitsOfMeasure = await _unitOfMeasureRepository.GetUnitsOfMeasurements();

//                if (/*requestRows == null ||*/ requests == null || items == null || unitsOfMeasure == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    var requestDtos = requests.ConvertToDto(items, unitsOfMeasure);
//                    return Ok(requestDtos);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
//            }
//        }


//        //[HttpGet]
//        //[Route("GetRequests")]
//        //public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequests()
//        //{
//        //    try
//        //    {
//        //        IEnumerable<Request> requests = await _requestRepository.GetRequestsWithRelatedData();

//        //        if (requests == null)
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            var requestDtos = requests.ConvertToRequestDTO();
//        //            return Ok(requestDtos);
//        //        }
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
//        //    }
//        //}
//    }
//}
