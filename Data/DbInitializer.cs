using GreensAndSips.Data;
using GreensAndSips.Models;
using System.Linq;

namespace GreensAndSips.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GreensAndSipsContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if any FoodItems already exist
            if (context.FoodItems.Any())
            {
                return; // DB has been seeded already
            }

            // Add seed data
            var foodItems = new FoodItem[]
            {
                new FoodItem { Item_name = "Salad", Item_desc = "Fresh greens", Price = 10.99M, Available = true, Vegetarian = true },
                new FoodItem { Item_name = "Burger", Item_desc = "Beef burger with fries", Price = 15.99M, Available = true, Vegetarian = false },
                new FoodItem { Item_name = "Pasta", Item_desc = "Creamy Alfredo pasta", Price = 12.99M, Available = true, Vegetarian = true }
            };

            foreach (var item in foodItems)
            {
                context.FoodItems.Add(item);
            }

            context.SaveChanges();
        }
    }
}
