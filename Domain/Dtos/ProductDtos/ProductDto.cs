using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ProductDtos;

public class ProductDto
{
    [Required] public string ProductName { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public int Quantity { get; set; }
    public string? Weight { get; set; }
    public string? Size { get; set; }
    [Required] public string Code { get; set; } = null!;
    [Required] public decimal Price { get; set; }
    [Required] public decimal DiscountPrice { get; set; }
    [Required] public int SubCategoryId { get; set; }
}