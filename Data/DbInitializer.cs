using GreensAndSips.Models;
using System.Linq;

namespace GreensAndSips.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GreensAndSipsContext context)
        {
            context.Database.EnsureCreated();

            if (context.FoodItems.Any())
            {
                return;
            }

            var foodItems = new FoodItem[]
            {
                new FoodItem { ItemName = "Shepherds Pie", ItemDesc = "Our tasty shepherds pie packed full of lean minced lamb and an assortment of vegetables", Available = true, Vegetarian = false, Price = 9.95M },
                new FoodItem { ItemName = "Cottage Pie", ItemDesc = "Our tasty cottage pie packed full of lean minced beef and an assortment of vegetables", Available = true, Vegetarian = false, Price = 9.95M },
                new FoodItem { ItemName = "Haggis, Neeps and Tatties", ItemDesc = "Scotland national Haggis dish. Sheep’s heart, liver, and lungs are minced, mixed with suet and oatmeal, then seasoned with onion, cayenne, and our secret spice. Served with boiled turnips and potatoes (‘neeps and tatties’)", Available = true, Vegetarian = false, Price = 11.50M },
                new FoodItem { ItemName = "Bangers and Mash", ItemDesc = "Succulent sausages nestled on a bed of buttery mashed potatoes and drenched in a rich onion gravy", Available = true, Vegetarian = false, Price = 8.50M },
                new FoodItem { ItemName = "Toad in the Hole", ItemDesc = "Ultimate toad-in-the-hole with caramelised onion gravy", Available = true, Vegetarian = false, Price = 7.50M }
            };

            context.FoodItems.AddRange(foodItems);
            context.SaveChanges();
        }
    }
}
