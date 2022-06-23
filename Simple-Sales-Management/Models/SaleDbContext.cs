using Microsoft.EntityFrameworkCore;

namespace Simple_Sales_Management.Models
{
    public class SaleDbContext : DbContext
    {
        public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options)
        {

        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
