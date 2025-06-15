using MediatR;

namespace OrderManagement.Application.Queries.Products
{
    public record GetProductByIdQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
