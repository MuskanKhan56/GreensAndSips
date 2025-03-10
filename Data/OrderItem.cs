using GreensAndSips.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreensAndSips.Models
{
    public class OrderItem
    {
        [Key] // ✅ This ensures OrderNo is part of the primary key
        public int OrderNo { get; set; }

        [Key] // ✅ This ensures StockID is also part of the composite primary key
        public int StockID { get; set; }

        public int Quantity { get; set; }

        // ✅ Foreign Key Relationships
        [ForeignKey("OrderNo")]
        public OrderHistory OrderHistory { get; set; }

        [ForeignKey("StockID")]
        public FoodItem FoodItem { get; set; }
    }
}
