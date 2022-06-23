using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Sales_Management.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        public double Amount { get; set; }
        [Required]
        [DisplayName("Sale Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SaleDate { get; set; }

        [DisplayName("Seller")]
        [Required]
        public int SellerForeignKey { get; set; }
        [ForeignKey("SellerForeignKey")]
        public Seller? Seller { get; set; }

    }
}
