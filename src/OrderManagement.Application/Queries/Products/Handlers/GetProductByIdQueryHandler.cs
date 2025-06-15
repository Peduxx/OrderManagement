using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Products.Handlers
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result>
    {
        private readonly IProductQueryService _productQueryService;

        public GetProductByIdQueryHandler(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                return Result.Failure(ProductErrors.ProductNotFound(request.Id));

            var data = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Code = product.Code,
            };

            return Result.Success().AddData(data);
        }
    }
}
