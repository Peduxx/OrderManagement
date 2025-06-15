using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface ICustomerQueryService
    {
        Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
    }
}
