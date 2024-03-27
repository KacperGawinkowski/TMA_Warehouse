namespace TMA_Warehouse.Client.FrontendModels
{
    public class ItemOrderModel
    {
        public string ItemName { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;
        public float Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string? Comment { get; set; }
    }
}
