using GreensAndSips.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BasketItem
{
    [Key, Column(Order = 1)]
    public int StockID { get; set; } // FK to FoodItem

    [Key, Column(Order = 2)]
    public int BasketID { get; set; } // FK to Basket

    [Required]
    public int Quantity { get; set; }

    [ForeignKey("BasketID")]
    public Basket? Basket { get; set; } // Nullable to prevent circular dependency

    [ForeignKey("StockID")]
    public FoodItem? FoodItem { get; set; } // Ensure this exists
}
