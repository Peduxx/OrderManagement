using MediatR;

namespace OrderManagement.Application.Queries.Products
{
    public record GetAllProductsQuery : IRequest<Result> { }
}
