using Domain.Dtos.BrandDtos;
using Domain.Response;

namespace Infrastructure.Services.BrandService;

public interface IBrandService
{
    Task<Response<List<GetBrandDto>>> GetBrands();
    Task<Response<GetBrandDto>> GetBrandById(int id);
    Task<Response<int>> AddBrand(AddBrandDto addBrand);
    Task<Response<int>> UpdateBrand(UpdateBrandDto addBrand);
    Task<Response<bool>> DeleteBrand(int id);
}