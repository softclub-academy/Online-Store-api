using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.ProductDtos;

public class GetProductDto : ProductDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Color { get; set; } = null!;
    public List<string> Images { get; set; } = null!;
}