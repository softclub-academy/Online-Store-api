using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ProductDtos;

public class UpdateProductDto : AddProductDto
{
    [Required]
    public int Id { get; set; }
}