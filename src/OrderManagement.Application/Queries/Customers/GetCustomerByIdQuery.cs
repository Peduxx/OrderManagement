using MediatR;

namespace OrderManagement.Application.Queries.Customers
{
    public record GetCustomerByIdQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
