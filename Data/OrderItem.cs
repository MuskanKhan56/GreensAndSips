using GreensAndSips.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreensAndSips.Models
{
    // Represents an item within an order
    public class OrderItem
    {
        [Key] // ✅ Part of the composite primary key
        public int OrderNo { get; set; } // Order number (linked to OrderHistory)

        [Key] // ✅ Part of the composite primary key
        public int StockID { get; set; } // Food item identifier

        public int Quantity { get; set; } // Number of units of this item in the order

        // ✅ Foreign Key Relationships
        [ForeignKey("OrderNo")]
        public OrderHistory OrderHistory { get; set; } // Reference to the associated order

        [ForeignKey("StockID")]
        public FoodItem FoodItem { get; set; } // Reference to the ordered food item
    }
}
