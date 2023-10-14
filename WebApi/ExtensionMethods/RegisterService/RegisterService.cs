using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Seed;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.BrandService;
using Infrastructure.Services.CatalogService;
using Infrastructure.Services.CategoryService;
using Infrastructure.Services.ColorService;
using Infrastructure.Services.ProductService;
using Infrastructure.Services.SmarphoneService;
using Infrastructure.Services.SubCategoryService;
using Infrastructure.Services.TelevisionService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISmartphoneService, SmartphoneService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ITelevisionService, TelevisionService>();
        services.AddScoped<Seeder>();
        
        services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false; // must have at least one digit
                config.Password.RequireNonAlphanumeric = false; // must have at least one non-alphanumeric character
                config.Password.RequireUppercase = false; // must have at least one uppercase character
                config.Password.RequireLowercase = false;  // must have at least one lowercase character
            })
            //for registering usermanager and signinmanger
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
    }
}