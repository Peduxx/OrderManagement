using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Infra.Data.Services.QueryServices
{
    public sealed class OrderQueryService : IOrderQueryService
    {
        private readonly OrderManagementContext _context;

        public OrderQueryService(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted, cancellationToken);
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => !o.IsDeleted)
                .ToListAsync(cancellationToken);
            return orders;
        }
    }
}
