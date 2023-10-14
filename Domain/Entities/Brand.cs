namespace Domain.Entities;

public class Brand
{
    public int Id { get; set; }
    public string BrandName { get; set; } = null!;
    public List<Product> Products { get; set; } = null!;
}