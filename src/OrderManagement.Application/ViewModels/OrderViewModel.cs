using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.ViewModels
{
    public record OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = [];
        public decimal TotalAmount { get; set; }
    }

}
