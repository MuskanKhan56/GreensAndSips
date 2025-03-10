using System.ComponentModel.DataAnnotations;

namespace GreensAndSips.Data
{
    public class OrderHistory
    {
        [Key, Required]
        public int OrderNo { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
