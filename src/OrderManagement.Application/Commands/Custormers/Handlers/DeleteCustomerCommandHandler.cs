using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Custormers.Handlers
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly ICustomerQueryService _customerQueryService;

        public DeleteCustomerCommandHandler(OrderManagementContext context, ICustomerQueryService customerQueryService)
        {
            _context = context;
            _customerQueryService = customerQueryService;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (customer == null)
                return Result.Failure(CustomersErrors.CustomerNotFound);

            customer.Delete();

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
