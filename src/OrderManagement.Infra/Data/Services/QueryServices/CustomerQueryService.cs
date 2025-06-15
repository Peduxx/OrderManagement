using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Infra.Data.Services.QueryServices
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly OrderManagementContext _context;

        public CustomerQueryService(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(customer => customer.Email == email && !customer.IsDeleted, cancellationToken);

            return customer;
        }

        public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                  .FirstOrDefaultAsync(customer => customer.Id == id && !customer.IsDeleted, cancellationToken);

            return customer;
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .Where(customer => !customer.IsDeleted)
                .ToListAsync(cancellationToken);

            return customers;
        }
    }
}
