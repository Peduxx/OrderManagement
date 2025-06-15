using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Products.Handlers
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly IProductQueryService _productQueryService;

        public DeleteProductCommandHandler(OrderManagementContext context, IProductQueryService productQueryService)
        {
            _context = context;
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                return Result.Failure(ProductErrors.ProductNotFound(request.Id));

            product.Delete();

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
