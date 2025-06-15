using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderQueryService
    {
        Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
    }
}
