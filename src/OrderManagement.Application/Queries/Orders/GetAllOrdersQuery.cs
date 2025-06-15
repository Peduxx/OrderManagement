using MediatR;

namespace OrderManagement.Application.Queries.Orders
{
    public record GetAllOrdersQuery : IRequest<Result> { }
}
