using Microsoft.EntityFrameworkCore;
using POC_TDD_App.Model;

namespace POC_TDD_App.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            _options = options;
        }

        public DbSet<CustomerModel> Customers => Set<CustomerModel>();
    }
}
