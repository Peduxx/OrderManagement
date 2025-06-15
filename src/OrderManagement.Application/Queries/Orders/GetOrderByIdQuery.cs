using MediatR;

namespace OrderManagement.Application.Queries.Orders
{
    public record GetOrderByIdQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
