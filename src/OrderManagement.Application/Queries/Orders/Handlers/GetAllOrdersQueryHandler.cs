using MediatR;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Orders.Handlers
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result>
    {
        private readonly IOrderQueryService _orderQueryService;

        public GetAllOrdersQueryHandler(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        public async Task<Result> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderQueryService.GetAllOrdersAsync(cancellationToken);

            var data = orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Items = order.OrderItems.Select(item => new OrderItemViewModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = order.TotalAmount,
            }).ToList();

            return Result.Success().AddData(data);
        }
    }
}
