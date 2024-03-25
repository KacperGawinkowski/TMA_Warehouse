//using Microsoft.AspNetCore.Mvc;
//using TMA_Warehouse.Server.Repositories;

//namespace TMA_Warehouse.Server.Controllers
//{
//    [ApiController]
//    [Route("Lists/Orders")]
//    public class RequestRowController : ControllerBase
//    {
//        private readonly ILogger<RequestController> _logger;
//        private readonly RequestRowRepository _requestRowRepository;
//        private readonly RequestRepository _requestRepository;
//        private readonly ItemRepository _itemRepository;
//        private readonly UnitOfMeasureRepository _unitOfMeasureRepository;

//        public RequestRowController(ILogger<RequestController> logger, RequestRepository requestRepository, RequestRowRepository requestRowRepository, ItemRepository itemRepository, UnitOfMeasureRepository unitOfMeasureRepository)
//        {
//            _logger = logger;
//            _requestRepository = requestRepository;
//            _requestRowRepository = requestRowRepository;
//            _itemRepository = itemRepository;
//            _unitOfMeasureRepository = unitOfMeasureRepository;
//        }
//    }
//}
