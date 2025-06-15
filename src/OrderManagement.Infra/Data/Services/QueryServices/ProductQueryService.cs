using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using System.Threading;

namespace OrderManagement.Infra.Data.Services.QueryServices
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly OrderManagementContext _context;

        public ProductQueryService(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == id && !product.IsDeleted, cancellationToken);

            return product;
        }

        public async Task<Product> GetByCodeAsync(long code, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Code == code, cancellationToken);

            return product;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(product => !product.IsDeleted)
                .ToListAsync(cancellationToken);

            return product;
        }

        public async Task<List<Product>> GetAnyByIdAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(product => ids.Contains(product.Id) && !product.IsDeleted)
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
