using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Models;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        private readonly WarehouseContext context;

        public OrderController(WarehouseContext context)
        {
            this.context = context;
        }
    }
}
