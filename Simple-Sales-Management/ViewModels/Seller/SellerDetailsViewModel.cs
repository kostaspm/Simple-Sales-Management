using System.ComponentModel;

namespace Simple_Sales_Management.ViewModels.Seller
{
    public class SellerDetailsViewModel
    {
        public int SellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MonthlyInfo> SellerDetails { get; set; }
    }

    public class MonthlyInfo
    {
        public int MonthId { get; set; }
        [DisplayName("Month")]
        public string MonthName { get; set; }
        [DisplayName("Total monthly sales")]
        public int MonthlySales { get; set; }
        [DisplayName("Total monthly commision")]
        public double MonthlyCommision { get; set; }
    }
}
