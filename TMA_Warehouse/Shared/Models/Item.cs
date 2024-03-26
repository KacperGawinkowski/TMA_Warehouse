using Shared.DTOs;
using System;
using System.Collections.Generic;

namespace WarehouseAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            OrderedItems = new HashSet<OrderedItem>();
        }

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

        public virtual ICollection<OrderedItem> OrderedItems { get; set; }

        public Item(ItemDTO itemDTO)
        {
            Id = itemDTO.Id;
            Name = itemDTO.Name;
            ItemGroup = itemDTO.ItemGroup;
            UnitOfMeasurement = itemDTO.UnitOfMeasurement;
            Quantity = itemDTO.Quantity;
            PriceWithoutVat = itemDTO.PriceWithoutVat;
            Status = itemDTO.Status;
            StorageLocation = itemDTO.StorageLocation;
            ContactPerson = itemDTO.ContactPerson;
            PhotoUrl = itemDTO.PhotoUrl;
        }
    }
}
