
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Selfra_Repositories.Base
{
    public class SelfraDBContextFactory : IDesignTimeDbContextFactory<SelfraDBContext>
    {
        public SelfraDBContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<SelfraDBContext>();

            builder.UseSqlServer("Server=.;Database=SelfRa_DB;uid=sa;pwd=1234567890;Trusted_Connection=True;TrustServerCertificate=True");

            return new SelfraDBContext(builder.Options);
        }
    }
}
