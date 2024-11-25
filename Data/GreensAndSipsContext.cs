using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreensAndSips.Models;

namespace GreensAndSips.Data
{
    public class GreensAndSipsContext : DbContext
    {
        public GreensAndSipsContext (DbContextOptions<GreensAndSipsContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>().ToTable("FoodItem");
        }
    }
}
