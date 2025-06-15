using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Commands.Orders
{
    public interface IOrderCommand
    {
        public Guid CustomerId { get; }
        public List<OrderItemDTO> Products { get; }
    }
}
