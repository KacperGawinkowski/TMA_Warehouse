using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA_Warehouse.Shared.DTOs;

namespace TMA_Warehouse.Shared.Models
{
    public class Request
    {
        public int Id { get; set; }
        //public int RequestRowID { get; set; }
        //public virtual RequestRow RequestRow { get; set; }
        public string EmployeeName { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public double Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string? Comment { get; set; }
        public string? Status { get; set; }

        public Request()
        {

        }

        public Request(RequestDTO requestDTO)
        {
            //Id = requestDTO.Id;
            //RequestRowID = requestDTO.RequestRowID;
            EmployeeName = requestDTO.EmployeeName;
            ItemId = requestDTO.ItemId;
            UnitOfMeasurementId = requestDTO.UnitOfMeasurementId;
            Quantity = requestDTO.Quantity;
            PriceWithoutVAT = requestDTO.PriceWithoutVAT;
            Comment = requestDTO.Comment;
            Status = requestDTO.Status;
        }
    }
}
