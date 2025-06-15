using MediatR;

namespace OrderManagement.Application.Commands.Custormers
{
    public record CreateCustomerCommand : IRequest<Result>, ICustomerCommand
    {
        public required string Name { get; set; }

        public required string Email { get; set; }
    }
}
