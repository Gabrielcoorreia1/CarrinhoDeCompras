using Microsoft.EntityFrameworkCore;
using PurchasingSystem.Domain.User.Entities;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public Task Delete(User user)
        {
            _context.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid userID, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userID, cancellationToken);
        }
        public Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(u => u.Email.Value == email, cancellationToken);
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}
