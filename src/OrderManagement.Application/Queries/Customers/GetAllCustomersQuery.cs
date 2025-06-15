using MediatR;

namespace OrderManagement.Application.Queries.Customers
{
    public record GetAllCustomersQuery : IRequest<Result> { }
}
