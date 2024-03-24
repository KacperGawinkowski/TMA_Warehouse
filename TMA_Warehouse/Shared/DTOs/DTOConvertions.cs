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
                        ItemGroupName = itemGroup.Name,
                        UnitOfMeasurementId = unitOfMeasurement.Id,
                        UnitOfMeasurementName = unitOfMeasurement.Name,
                        Quantity = item.Quantity,
                        PriceWithoutVAT = item.PriceWithoutVAT,
                        Status = item.Status,
                        StorageLocation = item.StorageLocation,
                        ContantPerson = item.ContantPerson,
                        Photo = item.PhotoURL
                    }).ToList();
        }

        public static ItemDTO ConvertToDto(this Item item, IEnumerable<ItemGroup> itemGroups, IEnumerable<UnitOfMeasurement> unitsOfMeasurements)
        {
            var items = new List<Item> { item };
            return ConvertToDto(items, itemGroups, unitsOfMeasurements).First();
        }
    }
}
