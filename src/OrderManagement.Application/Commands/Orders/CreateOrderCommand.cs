using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Commands.Orders
{
    public record CreateOrderCommand : IRequest<Result>, IOrderCommand
    {
        public required Guid CustomerId { get; set; }
        public required List<OrderItemDTO> Products { get; set; }
    }
}
