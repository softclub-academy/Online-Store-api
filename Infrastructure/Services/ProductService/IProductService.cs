using Domain.Dtos.ProductDtos;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<Response<GetProductPageDto>> GetProductPage(ProductFilter filter, string? userId);
    Task<Response<GetProductDto>> GetProductById(int id, string? userId);
    Task<Response<int>> AddProduct(AddProductDto addProduct, string userId);
    Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct, string userId);
    Task<Response<bool>> DeleteProduct(int id, string userId);
}