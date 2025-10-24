using Microsoft.EntityFrameworkCore;
using PurchasingSystem.Domain.Cart.Entities;
using PurchasingSystem.Domain.User.Entities;

namespace PurchasingSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}
