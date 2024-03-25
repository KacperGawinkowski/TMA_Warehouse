//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TMA_Warehouse.Shared.DTOs;

//namespace TMA_Warehouse.Shared.Models
//{
//    public class RequestRow
//    {
//        public int Id { get; set; }
//        public int RequestId { get; set; }
//        public virtual Request Request { get; set; }
//        public int ItemId { get; set; }
//        public virtual Item Item { get; set; }
//        public int UnitofMeasurementId { get; set; }
//        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
//        public double Quantity { get; set; }
//        public decimal PriceWithoutVAT { get; set; }
//        public string? Comment { get; set; }

//        public RequestRow()
//        {

//        }

//        public RequestRow(RequestRowDTO requestRowDTO)
//        {
//            Id = requestRowDTO.Id;
//            RequestId = requestRowDTO.RequestId;
//            ItemId = requestRowDTO.ItemId;
//            UnitofMeasurementId = requestRowDTO.UnitOfMeasurementId;
//            Quantity = requestRowDTO.Quantity;
//            PriceWithoutVAT = requestRowDTO.PriceWithoutVAT;
//            Comment = requestRowDTO.Comment;
//        }
//    }
//}
