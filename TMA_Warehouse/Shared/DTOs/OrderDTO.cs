using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseAPI.Models;

namespace TMA_Warehouse.Shared.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? Comment { get; set; }
        public IEnumerable<OrderedItemDTO> OrderedItems { get; set; }
    }
}
