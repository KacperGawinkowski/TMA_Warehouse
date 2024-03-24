using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Shared.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemGroupId { get; set; }
        public string ItemGroupName{ get; set; }
        public int UnitOfMeasurementId { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public double Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string Status { get; set; }
        public string? StorageLocation { get; set; }
        public string? ContantPerson { get; set; }
        public string? Photo { get; set; }
    }
}
