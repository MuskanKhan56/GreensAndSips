using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreensAndSips.Models
{
    public class FoodItem
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Item_name { get; set; }

        [StringLength(255)]
        public string Item_desc { get; set; }

        public bool? Available { get; set; }

        public bool? Vegetarian { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]                    
        public decimal? Price { get; set; }
    }
}
