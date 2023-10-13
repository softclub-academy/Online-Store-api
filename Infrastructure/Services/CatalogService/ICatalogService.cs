using Domain.Dtos.CatalogDtos;
using Domain.Response;

namespace Infrastructure.Services.CatalogService;

public interface ICatalogService
{
    Task<Response<List<GetCatalogDto>>> GetCatalogs();
    Task<Response<GetCatalogDto>> GetCatalogById(int id);
    Response<int> AddCatalog(AddCatalogDto addCatalog);
    Response<int> UpdateCatalog(UpdateCatalogDto updateCatalog);
    Response<bool> DeleteCatalog(int id);
}