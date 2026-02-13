using Microsoft.EntityFrameworkCore;
using PurchasingSystem.Domain.Cart.Entities;
using PurchasingSystem.Domain.User.Entities;

namespace PurchasingSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.UserId).IsRequired();
                
                entity.OwnsMany(c => c.Items, items =>
                {
                    items.WithOwner().HasForeignKey("CartId");
                    items.Property<int>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(i => i.ProductId).IsRequired();
                    items.Property(i => i.Quantity).IsRequired();
                });
            });
            
            // Configure User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
            });
        }
    }
}
