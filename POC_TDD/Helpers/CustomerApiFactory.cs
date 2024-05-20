using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POC_TDD_App;
using POC_TDD_App.Data;

namespace POC_TDD.Helpers
{
    public class CustomerApiFactory : WebApplicationFactory<IApiAssemblyMarker>
    {

        public AppDbContext AddCustomerDbContext()
        {
            var db=Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();
            db.Database.EnsureCreated();
            return db;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContextFactory<AppDbContext>(
                   o => o.UseInMemoryDatabase(databaseName: "Customers")
                    );
            });
        }
    }
}
