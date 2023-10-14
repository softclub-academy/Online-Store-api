namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public string? Weight { get; set; }
    public string? Size { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int ColorId { get; set; }
    public Color Color { get; set; } = null!;
}