using MediatR;
using OrderManagement.Application.Commands.Products.Validations;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Products.Handlers
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly IProductQueryService _productQueryService;

        public CreateProductCommandHandler(OrderManagementContext context, IProductQueryService productQueryService)
        {
            _context = context;
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = ProductValidation.Validate(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var alreadyCreatedProduct = await _productQueryService.GetByCodeAsync(request.Code, cancellationToken);

            if (alreadyCreatedProduct != null)
            {
                alreadyCreatedProduct.AddStock(request.Stock);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            var product = Product.Create(request.Name, request.Stock, request.Price, request.Code);

            await _context.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
