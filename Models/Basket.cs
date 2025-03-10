using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Represents a shopping basket for a user
public class Basket
{
    [Key] // ✅ Primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ Auto-increments the BasketID
    public int BasketID { get; set; } // Unique identifier for the basket
}
