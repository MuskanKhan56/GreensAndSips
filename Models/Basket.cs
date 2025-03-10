using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Basket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BasketID { get; set; }
}
