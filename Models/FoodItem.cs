using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreensAndSips.Models
{
    public class FoodItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ItemDesc { get; set; } = string.Empty;

        public bool Available { get; set; } = true;

        public bool Vegetarian { get; set; } = false;

        [Required]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; } = 0M;

        [StringLength(255)]
        public string ImageDescription { get; set; }
        public byte[] ImageData { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
