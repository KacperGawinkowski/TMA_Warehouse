namespace TMA_Warehouse.Shared.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemGroupId { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public double Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string Status { get; set; }
        public string? StorageLocation { get; set; }
        public string? ContantPerson { get; set; }
        public string? PhotoURL { get; set; }
    }
}
