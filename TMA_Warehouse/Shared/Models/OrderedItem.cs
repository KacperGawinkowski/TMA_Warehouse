using System;
using System.Collections.Generic;

namespace TMA_Warehouse.Shared.Models
{
    public partial class OrderedItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;
        public float Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string? Comment { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
