using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreensAndSips.Data
{
    // This class represents an item in the checkout process but is not mapped to the database
    [NotMapped]
    public class CheckoutItem
    {
        [Key, Required]
        public int ID { get; set; } // Unique identifier for the checkout item

        [Required]
        public decimal Price { get; set; } // Price of the item

        [Required]
        public string Item_Name { get; set; } // Name of the item

        [Required]
        public int Quantity { get; set; } // Quantity of the item in the order
    }
}
