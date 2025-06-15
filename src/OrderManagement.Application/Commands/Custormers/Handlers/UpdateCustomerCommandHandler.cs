using MediatR;
using OrderManagement.Application.Commands.Custormers.Validations;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Custormers.Handlers
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly ICustomerQueryService _customerQueryService;

        public UpdateCustomerCommandHandler(OrderManagementContext context, ICustomerQueryService customerQueryService)
        {
            _context = context;
            _customerQueryService = customerQueryService;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var validationResult = CustomerValidation.Validate(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var customer = await _customerQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (customer == null)
                return Result.Failure(CustomersErrors.CustomerNotFound);

            if (customer.Email != request.Email)
            {
                var emailExists = await _customerQueryService.GetByEmailAsync(request.Email, cancellationToken);

                if (emailExists != null)
                    return Result.Failure(CustomersErrors.EmailAlreadyExists);
            }

            customer.Update(request.Name, request.Email);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}