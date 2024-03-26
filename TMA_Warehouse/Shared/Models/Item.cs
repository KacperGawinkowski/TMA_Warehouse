namespace TMA_Warehouse.Shared.Models
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
    }
}
