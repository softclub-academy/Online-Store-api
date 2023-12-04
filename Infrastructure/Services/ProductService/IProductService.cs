using Domain.Dtos.ProductDtos;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<Response<GetProductPageDto>> GetProductPage(ProductFilter filter);
    Task<Response<GetProductDto>> GetProductById(int id);
    Task<Response<int>> AddProduct(AddProductDto addProduct, string user);
    Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct, string user);
    Task<Response<bool>> DeleteProduct(int id);
}