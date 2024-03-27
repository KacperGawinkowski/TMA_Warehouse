namespace TMA_Warehouse.Client.FrontendModels
{
    public class OrderForm
    {
        public int ItemId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public float Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string? Comment { get; set; }
    }
}
