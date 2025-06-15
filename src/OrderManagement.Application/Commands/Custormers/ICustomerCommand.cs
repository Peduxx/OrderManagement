namespace OrderManagement.Application.Commands.Custormers
{
    public interface ICustomerCommand
    {
        string Name { get; }
        string Email { get; }
    }
}
