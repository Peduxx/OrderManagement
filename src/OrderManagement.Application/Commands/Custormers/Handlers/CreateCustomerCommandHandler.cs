using MediatR;
using OrderManagement.Application.Commands.Custormers.Validations;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;

namespace OrderManagement.Application.Commands.Custormers.Handlers
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        private readonly OrderManagementContext _context;
        private readonly ICustomerQueryService _customerQueryService;

        public CreateCustomerCommandHandler(OrderManagementContext context, ICustomerQueryService customerQueryService)
        {
            _context = context;
            _customerQueryService = customerQueryService;
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var validationResult = CustomerValidation.Validate(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var customer = await _customerQueryService.GetByEmailAsync(request.Email, cancellationToken);

            if (customer != null)
                return Result.Failure(CustomersErrors.EmailAlreadyExists);

            customer = Customer.Create(request.Name, request.Email);

            await _context.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}