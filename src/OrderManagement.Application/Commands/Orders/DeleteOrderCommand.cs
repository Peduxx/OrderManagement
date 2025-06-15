using MediatR;

namespace OrderManagement.Application.Commands.Orders
{
    public record DeleteOrderCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
