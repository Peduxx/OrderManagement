using MediatR;
using OrderManagement.Application.Commands.Products.Validations;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Products.Handlers
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly IProductQueryService _productQueryService;

        public UpdateProductCommandHandler(OrderManagementContext context, IProductQueryService productQueryService)
        {
            _context = context;
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = ProductValidation.Validate(request);

            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var product = await _productQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                return Result.Failure(ProductErrors.ProductNotFound(request.Id));

            product.Update(request.Name, request.Stock, request.Price, request.Code);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
