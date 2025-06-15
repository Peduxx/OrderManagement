using MediatR;
using OrderManagement.Application.ViewModels;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Queries.Customers.Handlers
{
    public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result>
    {
        private readonly ICustomerQueryService _customerQueryService;

        public GetAllCustomersQueryHandler(ICustomerQueryService customerQueryService)
        {
            _customerQueryService = customerQueryService;
        }

        public async Task<Result> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerQueryService.GetAllAsync(cancellationToken);

            var data = customers.Select(customer => new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
            });

            return Result.Success().AddData(data);
        }
    }
}
