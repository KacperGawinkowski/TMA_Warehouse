namespace TMA_Warehouse.Shared.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderedItems = new HashSet<OrderedItem>();
        }

        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? Comment { get; set; }

        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
