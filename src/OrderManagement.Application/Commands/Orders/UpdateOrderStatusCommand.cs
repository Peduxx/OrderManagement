using MediatR;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Commands.Orders
{
    public class UpdateOrderStatusCommand : IRequest<Result>
    {
        public required Guid OrderId { get; set; }
        public required OrderStatus NewStatus { get; set; }
    }
}
