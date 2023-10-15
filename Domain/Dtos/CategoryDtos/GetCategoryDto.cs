using Domain.Dtos.SubCategoryDtos;

namespace Domain.Dtos.CategoryDtos;

public class GetCategoryDto : CategoryDto
{
    public int Id { get; set; }
    public List<GetSubCategoryDto> SubCategories { get; set; } = null!;
}