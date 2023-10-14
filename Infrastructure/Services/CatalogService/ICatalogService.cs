using Domain.Dtos.CatalogDtos;
using Domain.Response;

namespace Infrastructure.Services.CatalogService;

public interface ICatalogService
{
    Task<Response<List<GetCatalogDto>>> GetCatalogs();
    Task<Response<GetCatalogDto>> GetCatalogById(int id);
    Task<Response<int>> AddCatalog(AddCatalogDto addCatalog);
    Task<Response<int>> UpdateCatalog(UpdateCatalogDto updateCatalog);
    Task<Response<bool>> DeleteCatalog(int id);
}