using MediatR;
using OrderManagement.Application.Commands.Orders.Validations;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Orders.Handlers
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly ICustomerQueryService _customerQueryService;
        private readonly IProductQueryService _productQueryService;

        public CreateOrderCommandHandler(OrderManagementContext context, ICustomerQueryService customerQueryService, IProductQueryService productQueryService)
        {
            _context = context;
            _customerQueryService = customerQueryService;
            _productQueryService = productQueryService;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = OrderValidation.Validate(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            if (await _customerQueryService.GetByIdAsync(request.CustomerId, cancellationToken) == null)
                return Result.Failure(CustomersErrors.CustomerNotFound);

            var orderItems = new List<OrderItem>();

            foreach (var requestProduct in request.Products)
            {
                var product = await _productQueryService.GetByIdAsync(requestProduct.ProductId, cancellationToken);

                if (product == null)
                    return Result.Failure(ProductErrors.ProductNotFound(requestProduct.ProductId));

                var orderItem = OrderItem.Create(Guid.Empty, product, requestProduct.Quantity);

                product.RemoveStock(requestProduct.Quantity);

                orderItems.Add(orderItem);
            }

            var order = Order.Create(request.CustomerId, orderItems);

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}