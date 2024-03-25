using AntDesign;
using TMA_Warehouse.Shared.DTOs;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Models
{
    public class ItemFrontendModel
    {
        public string Size { get; set; } = AntSizeLDSType.Small;
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ItemGroupId { get; set; } = 1;
        public string ItemGroupName { get; set; } = "";
        public int UnitOfMeasurementId { get; set; } = 1;
        public string UnitOfMeasurementName { get; set; } = "";
        public double Quantity { get; set; } = 1;
        public decimal PriceWithoutVAT { get; set; } = 0m;
        public string Status { get; set; } = "";
        public string? StorageLocation { get; set; } = "";
        public string? ContactPerson { get; set; } = "";
        public string? PhotoURL { get; set; } = "";

        //public ItemFrontendModel(ItemDTO item, string itemGroupName, string unitOfMeasurementName)
        //{
        //    Name = item.Name;
        //    ItemGroupId = item.ItemGroupId;
        //    ItemGroupName = itemGroupName;
        //    UnitOfMeasurementId = item.UnitOfMeasurementId;
        //    UnitOfMeasurementName = unitOfMeasurementName;
        //    Quantity = item.Quantity;
        //    PriceWithoutVAT = item.PriceWithoutVAT;
        //    Status = item.Status;
        //    StorageLocation = item.StorageLocation;
        //    ContactPerson = item.ContantPerson;
        //    PhotoURL = item.PhotoURL;
        //}

        public ItemDTO ConvertToItemDto()
        {
            return new ItemDTO
            {
                Id = Id,
                Name = Name,
                ItemGroupId = ItemGroupId,
                UnitOfMeasurementId = UnitOfMeasurementId,
                Quantity = Quantity,
                PriceWithoutVAT = PriceWithoutVAT,
                Status = Status,
                StorageLocation = StorageLocation,
                ContantPerson = ContactPerson,
                PhotoURL = PhotoURL
            };
        }
    }
}
