namespace TMA_Warehouse.Shared.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? Comment { get; set; }
        public string? Status { get; set; } = "New";
        public List<OrderedItemDTO> OrderedItems { get; set; }
    }
}
