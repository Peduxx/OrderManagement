using MediatR;
using OrderManagement.Application.Errors.Contexts;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Customers.Handlers
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result>
    {
        private readonly ICustomerQueryService _customerQueryService;

        public GetCustomerByIdQueryHandler(ICustomerQueryService customerQueryService)
        {
            _customerQueryService = customerQueryService;
        }

        public async Task<Result> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerQueryService.GetByIdAsync(request.Id, cancellationToken);

            if (customer == null)
                return Result.Failure(CustomersErrors.CustomerNotFound);

            var data = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
            };

            return Result.Success().AddData(data);
        }
    }
}
