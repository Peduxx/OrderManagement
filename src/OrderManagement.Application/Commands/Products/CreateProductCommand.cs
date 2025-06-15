using MediatR;

namespace OrderManagement.Application.Commands.Products
{
    public sealed record CreateProductCommand : IRequest<Result>, IProductCommand
    {
        public required string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public long Code { get; set; }
    }
}
