using Domain.Dtos.CartDTOs;
using Domain.Dtos.ImageDTOs;
using Domain.Dtos.UserProfileDtos;

namespace Domain.Dtos.ProductDtos;

public class GetProductDto : ProductDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Color { get; set; } = null!;
    public bool ProductInMyCart { get; set; }
    public new List<GetImageDto> Images { get; set; } = null!;
    public List<GetUserShortInfoDto>? Users { get; set; }
    public CartDto? ProductInfoFromCart { get; set; }
}