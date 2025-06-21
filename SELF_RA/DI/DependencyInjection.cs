using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_Repositories.Base;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net.payOS;
namespace SELF_RA.DI
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigSwagger();
            services.AddAuthenJwt();
            services.AddDatabase(configuration);
            services.ConfigRoute();
            services.ConfigCors();
            //services.ConfigCorsSignalR();
            services.JwtSettingsConfig(configuration);
            services.AddIdentity(configuration);
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.NewsApiSettingsConfig(configuration);
            services.OpenAiSettingsConfig(configuration);
            services.AddPayOS(configuration);
        }
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
            })
             .AddEntityFrameworkStores<SelfraDBContext>()
             .AddDefaultTokenProviders();
        }
        public static void JwtSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(option =>
            {
                JwtSettings jwtSettings = new JwtSettings
                {
                    SecretKey = configuration.GetValue<string>("JwtSettings:SecretKey"),
                    Issuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                    Audience = configuration.GetValue<string>("JwtSettings:Audience"),
                    AccessTokenExpirationMinutes = configuration.GetValue<int>("JwtSettings:AccessTokenExpirationMinutes"),
                    RefreshTokenExpirationDays = configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays")
                };
                jwtSettings.IsValid();
                return jwtSettings;
            });

        }
        public static void ConfigCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }


        //public static void ConfigCorsSignalR(this IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("AllowSpecificOrigin",
        //            builder =>
        //            {
        //                builder.WithOrigins("https://localhost:7016")
        //                       .AllowAnyHeader()
        //                       .AllowAnyMethod()
        //                       .AllowCredentials();
        //            });
        //    });
        //}
        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        public static void AddAuthenJwt(this IServiceCollection services)
        {
            JwtSettings jwtSettings = new JwtSettings();
            services.AddAuthentication(e =>
            {
                e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(e =>
            {
                e.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey!))

                };
                e.SaveToken = true;
                e.RequireHttpsMetadata = true;
            });
        }
        public static void ConfigSwagger(this IServiceCollection services)
        {
            // config swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API"

                });

                // Tùy chỉnh Swagger để hỗ trợ TimeOnly dưới dạng chuỗi
                c.MapType<TimeOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "time",
                    Example = new OpenApiString("00:00:00")
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Thêm JWT Bearer Token vào Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header sử dụng scheme Bearer.",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Name = "Authorization",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                c.OrderActionsBy((apiDesc) =>
                {
                    if (apiDesc.HttpMethod == "POST") return "3";
                    if (apiDesc.HttpMethod == "GET") return "1";
                    if (apiDesc.HttpMethod == "PUT") return "2";
                    if (apiDesc.HttpMethod == "DELETE") return "4";
                    return "5";
                });
            });
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SelfraDBContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
        public static void OpenAiSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(option =>
            {
                var settings = new OpenAISettings
                {
                    ApiKey = configuration.GetValue<string>("OpenAI:ApiKey") ?? string.Empty
                };

                if (string.IsNullOrEmpty(settings.ApiKey))
                    throw new Exception("OpenAI:ApiKey không được để trống trong cấu hình.");

                return settings;
            });
        }

        public static void NewsApiSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(option =>
            {
                var settings = new AINewsSettings
                {
                    ApiKey = configuration.GetValue<string>("NewsApi:ApiKey") ?? string.Empty
                };

                if (string.IsNullOrEmpty(settings.ApiKey))
                    throw new Exception("NewsApi:ApiKey không được để trống trong cấu hình.");

                return settings;
            });
        }
        public static void AddPayOS(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PayOSOptions>(configuration.GetSection("PayOS"));

            services.AddScoped<PayOS>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<PayOSOptions>>().Value;
                return new PayOS(
                    options.ClientId,
                    options.ApiKey,
                    options.ChecksumKey
                );
            });

        }
    }
}
