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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseProgressService, CourseProgressService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuizzService, QuizzService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFoodDetailService, FoodDetailService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IMentorService, MentorService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMentorContactService, MentorContactService>();
            services.AddScoped<IPostVoteService, PostVoteService>();
        }
    }
}
