using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IProductQueryService
    {
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Product> GetByCodeAsync(long code, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Product>> GetAnyByIdAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
