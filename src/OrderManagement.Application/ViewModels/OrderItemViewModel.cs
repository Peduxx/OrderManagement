namespace OrderManagement.Application.ViewModels
{
    public record OrderItemViewModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
