using Domain.Dtos.ProductDtos;
using Domain.Response;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<Response<List<GetProductsDto>>> GetProducts();
    Task<Response<GetProductDto>> GetProductById(int id);
    Task<Response<int>> AddProduct(AddProductDto addProduct, string user);
    Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct, string user);
    Task<Response<bool>> DeleteProduct(int id);
}