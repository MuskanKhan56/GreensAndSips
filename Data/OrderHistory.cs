using System.ComponentModel.DataAnnotations;

namespace GreensAndSips.Data
{
    // Represents a record of a completed order
    public class OrderHistory
    {
        [Key, Required]
        public int OrderNo { get; set; } // Unique identifier for the order

        [Required]
        public string Email { get; set; } // Email of the customer who placed the order
    }
}
