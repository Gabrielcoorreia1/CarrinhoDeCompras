using Microsoft.EntityFrameworkCore;

namespace PurchasingSystem.Infrastructure.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
        }

        public async Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }

        public Task RemoveAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            return Task.CompletedTask;
        }
    }
}
