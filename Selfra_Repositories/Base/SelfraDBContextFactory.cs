
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Selfra_Repositories.Base
{
    public class SelfraDBContextFactory : IDesignTimeDbContextFactory<SelfraDBContext>
    {
        public SelfraDBContext CreateDbContext(string[] args)
        {

            //var builder = new DbContextOptionsBuilder<SelfraDBContext>();

            //builder.UseSqlServer("Server=.;Database=SelfRa_DB;uid=sa;pwd=1234567890;Trusted_Connection=True;TrustServerCertificate=True");

            //return new SelfraDBContext(builder.Options);
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<SelfraDBContext>();
            builder.UseSqlServer(connectionString);

            return new SelfraDBContext(builder.Options);
        }
    }

}
