namespace Domain.Entities;

public class SubCategory
{
    public int Id { get; set; }
    public string SubCategoryName { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<Smartphone> Smartphones { get; set; } = null!;
    public List<Television> Televisions { get; set; } = null!;
    public List<Product> Products { get; set; } = null!;
}