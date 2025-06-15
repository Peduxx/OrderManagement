using MediatR;

namespace OrderManagement.Application.Commands.Products
{
    public record DeleteProductCommand : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }
}
