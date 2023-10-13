using System.Net;
using Domain.Dtos.CatalogDtos;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CatalogService;

public class CatalogService(ApplicationContext context) : ICatalogService
{
    public async Task<Response<List<GetCatalogDto>>> GetCatalogs()
    {
        try
        {
            var catalogs = await context.Catalogs.Select(c => new GetCatalogDto()
            {
                Id = c.Id,
                CatalogName = c.CatalogName
            }).ToListAsync();
            return new Response<List<GetCatalogDto>>(catalogs);
        }
        catch (Exception e)
        {
            return new Response<List<GetCatalogDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCatalogDto>> GetCatalogById(int id)
    {
        try
        {
            var catalog = await context.Catalogs.Select(c => new GetCatalogDto()
            {
                Id = c.Id,
                CatalogName = c.CatalogName
            }).FirstOrDefaultAsync(c => c.Id == id);
            if (catalog == null) return new Response<GetCatalogDto>(HttpStatusCode.BadRequest, "Catalog not found!");
            return new Response<GetCatalogDto>(catalog);
        }
        catch (Exception e)
        {
            return new Response<GetCatalogDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public Response<int> AddCatalog(AddCatalogDto addCatalog)
    {
        throw new NotImplementedException();
    }

    public Response<int> UpdateCatalog(UpdateCatalogDto updateCatalog)
    {
        throw new NotImplementedException();
    }

    public Response<bool> DeleteCatalog(int id)
    {
        throw new NotImplementedException();
    }
}