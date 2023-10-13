using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ProductDtos;

public class AddProductDto : ProductDto
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public int BrandId { get; set; }
    [Required]
    public int ColorId { get; set; }
}