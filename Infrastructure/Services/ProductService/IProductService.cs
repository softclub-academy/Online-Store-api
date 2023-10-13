using Domain.Dtos.ProductDtos;
using Domain.Response;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<Response<List<GetProductDto>>> GetProducts();
    Task<Response<GetProductDto>> GetProductById(int id);
    Task<Response<int>> AddProduct(AddProductDto addProduct);
    Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct);
    Task<Response<bool>> DeleteProduct(int id);
}