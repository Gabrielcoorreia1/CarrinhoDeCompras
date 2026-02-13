using Microsoft.EntityFrameworkCore;
using PurchasingSystem.Domain.Cart.Interfaces;

namespace PurchasingSystem.Infrastructure.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        
        public async Task AddAsync(Domain.Cart.Entities.Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
        }

        public async Task<Domain.Cart.Entities.Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }
        
        public async Task<Domain.Cart.Entities.Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public Task RemoveAsync(Domain.Cart.Entities.Cart cart)
        {
            _context.Carts.Remove(cart);
            return Task.CompletedTask;
        }
        
        public Task UpdateAsync(Domain.Cart.Entities.Cart cart)
        {
            _context.Carts.Update(cart);
            return Task.CompletedTask;
        }
    }
}
