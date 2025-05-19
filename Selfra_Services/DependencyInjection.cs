using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Selfra_Contract_Services.Interface;
using Selfra_Repositories.Repositories;
using Selfra_Services.Service;
using Selft.Contract.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Selfra_Services
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepository();
            services.AddAutoMapper();
            services.AddServices(configuration);
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddMemoryCache();

        }
        public static void AddRepository(this IServiceCollection services)
        {
            services
               .AddScoped<IUnitOfWork, UnitOfWork>();
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
