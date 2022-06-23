using Simple_Sales_Management.Models;

namespace Simple_Sales_Management.Services.CommisionServices
{
    public interface ICommisionService
    {
        double CalculateCommision(Seller seller);

        double CalculateMonthlyCommision(IEnumerable<double> amounts);
    }
}
