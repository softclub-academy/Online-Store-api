namespace Domain.Dtos.ProductDtos;

public class GetProductsDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string Image { get; set; } = null!;
    public decimal Price { get; set; }
}