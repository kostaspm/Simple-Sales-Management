using Simple_Sales_Management.Models;

namespace Simple_Sales_Management.Services.CommisionServices
{
    public class CommisionService : ICommisionService
    {
        private readonly SaleDbContext _context;

        public CommisionService(SaleDbContext context)
        {
            _context = context;
        }

        public double CalculateCommision(Seller seller)
        {
            double totalCommision = 0;

            if (seller.Sales != null)
            {
                var SellerSales = seller.Sales.ToList();
                
                foreach (var sale in SellerSales)
                {
                    totalCommision += sale.Amount * 10 / 100;
                }
            }
            
            return totalCommision;
        }

        public double CalculateMonthlyCommision(IEnumerable<double> amounts)
        {
            double totalMonthlyCommision = 0;
            if (amounts != null)
            {
                foreach(var amount in amounts)
                {
                    totalMonthlyCommision += amount * 10 / 100;
                }
            }
            return totalMonthlyCommision;
        }
    }
}
