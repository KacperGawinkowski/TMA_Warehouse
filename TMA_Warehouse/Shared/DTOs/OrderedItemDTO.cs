using System;
using System.Collections.Generic;
using System.Linq;
namespace TMA_Warehouse.Shared.DTOs
{
    public class OrderedItemDTO
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string? Comment { get; set; }
    }
}
