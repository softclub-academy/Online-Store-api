using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.CatalogDtos;

public class CatalogDto
{
    [Required]
    public string CatalogName { get; set; } = null!;
}