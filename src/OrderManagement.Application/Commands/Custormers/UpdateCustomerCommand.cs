using MediatR;

namespace OrderManagement.Application.Commands.Custormers
{
    public record UpdateCustomerCommand : IRequest<Result>, ICustomerCommand
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
