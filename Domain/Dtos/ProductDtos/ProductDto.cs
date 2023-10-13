using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ProductDtos;

public class ProductDto
{
    [Required]
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Weight { get; set; }
    public string? Size { get; set; }
}