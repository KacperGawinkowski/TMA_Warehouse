using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Shared.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        //public int RequestRowID { get; set; }
        public string EmployeeName { get; set; }
        public int ItemId { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public double Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string? Comment { get; set; }
        public string? Status { get; set; }

        public RequestDTO()
        {
            
        }

        public RequestDTO(Request request)
        {
            Id = request.Id;
            //RequestRowID = request.RequestRowID;
            EmployeeName = request.EmployeeName;
            ItemId = request.ItemId;
            UnitOfMeasurementId = request.UnitOfMeasurementId;
            Quantity = request.Quantity;
            PriceWithoutVAT = request.PriceWithoutVAT;
            Comment = request.Comment;
            Status = request.Status;
        }
    }
}
