using System;
using System.Collections.Generic;
using System.Text;
using WarehouseAPI.Models;

namespace Shared.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ItemGroup { get; set; } = null!;
        public string UnitOfMeasurement { get; set; } = null!;
        public float Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string Status { get; set; } = null!;
        public string? StorageLocation { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhotoUrl { get; set; }

        public ItemDTO()
        {

        }

        public ItemDTO(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            ItemGroup = item.ItemGroup;
            UnitOfMeasurement = item.UnitOfMeasurement;
            Quantity = item.Quantity;
            PriceWithoutVat = item.PriceWithoutVat;
            Status = item.Status;
            StorageLocation = item.StorageLocation;
            ContactPerson = item.ContactPerson;
            PhotoUrl = item.PhotoUrl;
        }
    }
}
