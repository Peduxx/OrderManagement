using MediatR;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Orders.Handlers
{
    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly OrderManagementContext _context;

        public UpdateOrderStatusCommandHandler(IOrderQueryService orderQueryService, OrderManagementContext context)
        {
            _orderQueryService = orderQueryService;
            _context = context;
        }

        public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryService.GetOrderByIdAsync(request.OrderId, cancellationToken);

            order.UpdateStatus(request.NewStatus);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
