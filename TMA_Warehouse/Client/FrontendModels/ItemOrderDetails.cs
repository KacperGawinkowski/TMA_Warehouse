namespace TMA_Warehouse.Client.FrontendModels
{
    public class ItemOrderDetails
    {
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public float Quantity { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public string? Comment { get; set; }
    }
}
