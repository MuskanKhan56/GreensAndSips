using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Models;

namespace GreensAndSips.Data
{
    // Represents the database context for the application, including Identity and custom tables
    public class GreensAndSipsContext : IdentityDbContext<IdentityUser>
    {
        // Constructor initializing the database context with options
        public GreensAndSipsContext(DbContextOptions<GreensAndSipsContext> options)
            : base(options)
        {
        }

        // Define database tables as DbSet properties
        public DbSet<FoodItem> FoodItems { get; set; } = default!; // Stores food items
        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; } = default!; // Stores checkout customers
        public DbSet<Basket> Baskets { get; set; } = default!; // Stores baskets
        public DbSet<BasketItem> BasketItems { get; set; } = default!; // Stores items inside baskets
        public DbSet<OrderHistory> OrderHistories { get; set; } = default!; // Stores order history
        public DbSet<OrderItem> OrderItems { get; set; } = default!; // Stores individual order items

        // Configuring entity relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map FoodItem entity to a table named "FoodItem"
            modelBuilder.Entity<FoodItem>().ToTable("FoodItem");

            // ✅ Enable Cascade Delete for BasketItems when the related FoodItem is deleted
            modelBuilder.Entity<BasketItem>()
                .HasOne(b => b.FoodItem)
                .WithMany()
                .HasForeignKey(b => b.StockID)
                .OnDelete(DeleteBehavior.Cascade); // Automatically deletes BasketItems when a FoodItem is removed

            // ✅ Composite key for BasketItem (BasketID and StockID together must be unique)
            modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });

            // ✅ Composite key for OrderItem (OrderNo and StockID together must be unique)
            modelBuilder.Entity<OrderItem>().HasKey(t => new { t.OrderNo, t.StockID });

            // ✅ Define foreign key relationships for OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.OrderHistory) // Each OrderItem belongs to one OrderHistory
                .WithMany()
                .HasForeignKey(oi => oi.OrderNo);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.FoodItem) // Each OrderItem is linked to a FoodItem
                .WithMany()
                .HasForeignKey(oi => oi.StockID);
        }

        // Represents application-wide settings stored in the database
        public class ApplicationSetting
        {
            public int Id { get; set; } // Unique identifier
            public string SettingName { get; set; } // Name of the setting
        }

        // Table for storing application settings
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
    }
}
