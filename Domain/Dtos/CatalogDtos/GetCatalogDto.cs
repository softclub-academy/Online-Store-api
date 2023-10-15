using Domain.Dtos.CategoryDtos;

namespace Domain.Dtos.CatalogDtos;

public class GetCatalogDto : CatalogDto
{
    public int Id { get; set; }
    public List<GetCategoryDto> Categories { get; set; } = null!;
}