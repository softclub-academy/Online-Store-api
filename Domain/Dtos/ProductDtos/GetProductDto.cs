using Domain.Dtos.CartDTOs;
using Domain.Dtos.SmartphoneDtos;
using Domain.Dtos.UserProfileDtos;

namespace Domain.Dtos.ProductDtos;

public class GetProductDto : ProductDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Color { get; set; } = null!;
    public List<string> Images { get; set; } = null!;
    public List<GetUserShortInfoDto>? Users { get; set; }
    public CartDto? ProductInfoFromCart { get; set; }
}