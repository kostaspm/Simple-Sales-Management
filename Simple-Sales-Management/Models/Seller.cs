using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Sales_Management.Models
{
    public class Seller
    {
        public Seller()
        {
            Sales = new List<Sale>();
        }

        [Key]
        public int SellerId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }


        public List<Sale>? Sales { get; set; }
    }
}
