using System.ComponentModel;

namespace Simple_Sales_Management.ViewModels.Seller
{
    public class SellerIndexViewModel
    {
        public int SellerId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public double Commision { get; set; }
    }
}
