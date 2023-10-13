using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.CatalogDtos;

public class UpdateCatalogDto : AddCatalogDto
{
    [Required]
    public int Id { get; set; }
}