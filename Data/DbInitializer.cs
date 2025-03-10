using GreensAndSips.Models;
using System.Linq;

namespace GreensAndSips.Data
{
    public static class DbInitializer
    {
        // Initializes the database with default data
        public static void Initialize(GreensAndSipsContext context)
        {
            // Ensure the database is created before adding data
            context.Database.EnsureCreated();

            // Check if any food items already exist in the database
            if (context.FoodItems.Any())
            {
                return; // Exit if data already exists
            }

            // Define an array of FoodItem objects to seed the database
            var foodItems = new FoodItem[]
            {
                new FoodItem { ItemName = "Shepherds Pie", ItemDesc = "Our tasty shepherds pie packed full of lean minced lamb and an assortment of vegetables", Available = true, Vegetarian = false, Price = 9.95M },
                new FoodItem { ItemName = "Cottage Pie", ItemDesc = "Our tasty cottage pie packed full of lean minced beef and an assortment of vegetables", Available = true, Vegetarian = false, Price = 9.95M },
                new FoodItem { ItemName = "Haggis, Neeps and Tatties", ItemDesc = "Scotland national Haggis dish. Sheep’s heart, liver, and lungs are minced, mixed with suet and oatmeal, then seasoned with onion, cayenne, and our secret spice. Served with boiled turnips and potatoes (‘neeps and tatties’)", Available = true, Vegetarian = false, Price = 11.50M },
                new FoodItem { ItemName = "Bangers and Mash", ItemDesc = "Succulent sausages nestled on a bed of buttery mashed potatoes and drenched in a rich onion gravy", Available = true, Vegetarian = false, Price = 8.50M },
                new FoodItem { ItemName = "Toad in the Hole", ItemDesc = "Ultimate toad-in-the-hole with caramelised onion gravy", Available = true, Vegetarian = false, Price = 7.50M }
            };

            // Add the food items to the database
            context.FoodItems.AddRange(foodItems);

            // Save changes to persist the data
            context.SaveChanges();
        }
    }
}
