using System.ComponentModel;

namespace Simple_Sales_Management.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        [DisplayName("Total number of sellers")]
        public int NumberOfSellers { get; set; }
        [DisplayName("Total number of sales")]
        public int NumberOfSales { get; set; }
    }
}
