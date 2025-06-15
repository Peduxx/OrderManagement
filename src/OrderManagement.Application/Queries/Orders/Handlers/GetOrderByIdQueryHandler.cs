using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Orders.Handlers
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result>
    {
        private readonly IOrderQueryService _orderQueryService;

        public GetOrderByIdQueryHandler(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        public async Task<Result> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryService.GetOrderByIdAsync(request.Id, cancellationToken);

            if (order == null)
                return Result.Failure(OrderErrors.OrderNotFound);

            var data = new OrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Items = order.OrderItems
                .Select(item => new OrderItemViewModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = order.TotalAmount
            };

            return Result.Success().AddData(data);
        }
    }
}
