using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }

    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Catalog> Catalogs { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Smartphone> Smartphones { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
    public DbSet<Television> Televisions { get; set; } = null!;
    public new DbSet<User> Users { get; set; } = null!;
}