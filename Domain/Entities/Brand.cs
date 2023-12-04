using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("BrandName", IsUnique = true)]
public class Brand
{
    public int Id { get; set; }
    public string BrandName { get; set; } = null!;
    public List<Product> Products { get; set; } = null!;
}