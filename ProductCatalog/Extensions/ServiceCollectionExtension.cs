using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Repositories;
using ProductCatalogCore.Interfaces;
using ProductCatalogInfrastructure.Data;
using ProductCatalogInfrastructure.Services;

namespace ProductCatalog.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCoreScopedConfig(this IServiceCollection services)
        {
            // Add Helper
            _ = services.AddScoped<IRandomizer, Randomizer>();
            _ = services.AddScoped<ICryptography, Cryptography>();
            _ = services.AddScoped<CategoryRepo>();
            _ = services.AddScoped<SubCategoryRepo>();
            _ = services.AddScoped<BrandRepo>();
            _ = services.AddScoped<ProductRepo>();

            return services;
        }

        public static IServiceCollection AddDbAndIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Project DbContext -- SQL Server Connection
            _ = services.AddDbContext<ProductCatalogContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging(), ServiceLifetime.Scoped);

            // Identity DbContext -- SQL Server Connection
            _ = services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // For Identity User and Role Purpose
            _ = services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddMiscConfig(this IServiceCollection services)
        {
            // Add Miscellaneous
            _ = services.AddHttpContextAccessor();

            // API Lower Case Url
            _ = services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }
    }
}
