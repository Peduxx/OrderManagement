using MediatR;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Products.Handlers
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result>
    {
        private readonly IProductQueryService _productQueryService;

        public GetAllProductsQueryHandler(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var customers = await _productQueryService.GetAllAsync(cancellationToken);

            var data = customers.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Code = product.Code
            });

            return Result.Success().AddData(data);
        }
    }
}
