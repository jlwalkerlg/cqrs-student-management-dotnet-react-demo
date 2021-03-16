using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Api;
using StudentManagement.Infrastructure.Data.EF;

namespace StudentManagement.ApiTests.Functional
{
    public class WebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the AppDbContext registration.
                var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register AppDbContext using the test database.
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDB"));
            });
        }
    }
}
