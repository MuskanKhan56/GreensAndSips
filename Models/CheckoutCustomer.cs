using System.ComponentModel.DataAnnotations;

public class CheckoutCustomer
{
    [Key]
    public string Email { get; set; }  // Primary key (user's email)

    public string Name { get; set; }

    public int BasketID { get; set; }  // Links to the user's basket
}
