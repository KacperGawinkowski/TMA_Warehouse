using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Shared.DTOs
{
    public static class DTOConvertions
    {
        public static IEnumerable<ItemDTO> ConvertToDto(this IEnumerable<Item> items, IEnumerable<ItemGroup> itemGroups, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        {
            return (from item in items
                    join itemGroup in itemGroups on item.ItemGroupId equals itemGroup.Id
                    join unitOfMeasurement in unitsOfMeasurements on item.UnitOfMeasurementId equals unitOfMeasurement.Id
                    select new ItemDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ItemGroupId = itemGroup.Id,
                        //ItemGroupName = itemGroup.Name,
                        UnitOfMeasurementId = unitOfMeasurement.Id,
                        //UnitOfMeasurementName = unitOfMeasurement.Name,
                        Quantity = item.Quantity,
                        PriceWithoutVAT = item.PriceWithoutVAT,
                        Status = item.Status,
                        StorageLocation = item.StorageLocation,
                        ContantPerson = item.ContantPerson,
                        PhotoURL = item.PhotoURL
                    }).ToList();
        }

        public static ItemDTO ConvertToDto(this Item item, IEnumerable<ItemGroup> itemGroups, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        {
            var items = new List<Item> { item };
            return ConvertToDto(items, itemGroups, unitsOfMeasurements).First();
        }

        public static IEnumerable<RequestDTO> ConvertToDto(this IEnumerable<Request> requests, /*IEnumerable<RequestRow> requestRows,*/ IEnumerable<Item> items, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        {
            return (from request in requests
                    join item in items on request.ItemId equals item.Id
                    /*join requestRow in requestRows on request.RequestRowID equals requestRow.Id*/
                    join unitOfMeasurement in unitsOfMeasurements on item.UnitOfMeasurementId equals unitOfMeasurement.Id
                    select new RequestDTO
                    {
                        Id = request.Id,
                        /*RequestRowID = requestRow.Id,*/
                        EmployeeName = request.EmployeeName,
                        ItemId = item.Id,
                        UnitOfMeasurementId = unitOfMeasurement.Id,
                        Quantity = request.Quantity,
                        PriceWithoutVAT = request.PriceWithoutVAT,
                        Comment = request.Comment,
                        Status = request.Status,
                    }).ToList();
        }

        public static RequestDTO ConvertToDto(this Request request, IEnumerable<Item> items, /*IEnumerable<RequestRow> requestRows,*/ IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        {
            var requests = new List<Request> { request };
            return ConvertToDto(requests, /*requestRows,*/ items, unitsOfMeasurements).First();
        }

        public static IEnumerable<RequestDTO> ConvertToRequestDTO(this IEnumerable<Request> requests)
        {
            return requests.Select(request => new RequestDTO(request));
        }





        //public static IEnumerable<RequestRowDTO> ConvertToDto(this IEnumerable<RequestRow> requestRows, IEnumerable<Request> requests, IEnumerable<Item> items, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        //{
        //    return (from requestRow in requestRows
        //            join item in items on requestRow.ItemId equals item.Id
        //            join request in requests on requestRow.RequestId equals request.Id
        //            join unitOfMeasurement in unitsOfMeasurements on item.UnitOfMeasurementId equals unitOfMeasurement.Id
        //            select new RequestRowDTO
        //            {
        //                Id = request.Id,
        //                RequestId = request.Id,
        //                ItemId = item.Id,
        //                UnitOfMeasurementId = unitOfMeasurement.Id,
        //                Quantity = request.Quantity,
        //                PriceWithoutVAT = request.PriceWithoutVAT,
        //                Comment = request.Comment,
        //            }).ToList();
        //}

        //public static RequestRowDTO ConvertToDto(this RequestRow requestRow, IEnumerable<Request> requests, IEnumerable<Item> items, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        //{
        //    var requestRows = new List<RequestRow> { requestRow };
        //    return ConvertToDto(requestRows, requests, items , unitsOfMeasurements).First();
        //}
    }
}
