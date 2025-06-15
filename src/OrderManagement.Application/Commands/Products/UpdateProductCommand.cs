using MediatR;

namespace OrderManagement.Application.Commands.Products
{
    public record UpdateProductCommand : IRequest<Result>, IProductCommand
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public long Code { get; set; }
    }
}
