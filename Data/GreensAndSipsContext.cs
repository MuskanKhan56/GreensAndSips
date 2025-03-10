using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Models;

namespace GreensAndSips.Data
{
    public class GreensAndSipsContext : IdentityDbContext<IdentityUser>
    {
        public GreensAndSipsContext(DbContextOptions<GreensAndSipsContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; } = default!;
        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; } = default!;
        public DbSet<Basket> Baskets { get; set; } = default!;
        public DbSet<BasketItem> BasketItems { get; set; } = default!;
        public DbSet<OrderHistory> OrderHistories { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodItem>().ToTable("FoodItem");

            // ✅ Enable Cascade Delete for BasketItems
            modelBuilder.Entity<BasketItem>()
                .HasOne(b => b.FoodItem)
                .WithMany()
                .HasForeignKey(b => b.StockID)
                .OnDelete(DeleteBehavior.Cascade); // 🔹 This automatically deletes BasketItems when FoodItem is deleted

            modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });

            // ✅ Composite key for OrderItem
            modelBuilder.Entity<OrderItem>().HasKey(t => new { t.OrderNo, t.StockID });

            // ✅ Foreign key relationships for OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.OrderHistory)
                .WithMany()
                .HasForeignKey(oi => oi.OrderNo);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.FoodItem)
                .WithMany()
                .HasForeignKey(oi => oi.StockID);
        }

        public class ApplicationSetting
        {
            public int Id { get; set; }
            public string SettingName { get; set; }
        }

        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
    }
}
