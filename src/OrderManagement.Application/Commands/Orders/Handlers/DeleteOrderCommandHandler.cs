using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Orders.Handlers
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly IOrderQueryService _orderQueryService;

        public DeleteOrderCommandHandler(OrderManagementContext context, IOrderQueryService orderQueryService)
        {
            _context = context;
            _orderQueryService = orderQueryService;
        }

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryService.GetOrderByIdAsync(request.Id, cancellationToken);

            if (order == null)
                return Result.Failure(OrderErrors.OrderNotFound);

            order.DeleteOrderAndItems();

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
