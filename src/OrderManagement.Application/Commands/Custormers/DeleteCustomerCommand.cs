using MediatR;

namespace OrderManagement.Application.Commands.Custormers
{
    public record DeleteCustomerCommand : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }
}
