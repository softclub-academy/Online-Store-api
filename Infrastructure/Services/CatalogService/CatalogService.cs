using System.Net;
using Domain.Dtos.CatalogDtos;
using Domain.Dtos.CategoryDtos;
using Domain.Dtos.SubCategoryDtos;
using Domain.Entities;
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
                CatalogName = c.CatalogName,
                Categories = c.Categories.Select(category => new GetCategoryDto()
                {
                    Id = category.Id,
                    CatalogId = category.CatalogId,
                    CategoryName = category.CategoryName,
                    SubCategories = category.SubCategories.Select(s => new GetSubCategoryDto()
                    {
                        Id = s.Id,
                        CategoryId = s.CategoryId,
                        SubCategoryName = s.SubCategoryName
                    }).ToList()
                }).ToList()
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

    public async Task<Response<int>> AddCatalog(AddCatalogDto addCatalog)
    {
        try
        {
            var catalog = new Catalog()
            {
                CatalogName = addCatalog.CatalogName
            };
            await context.Catalogs.AddAsync(catalog);
            await context.SaveChangesAsync();
            return new Response<int>(catalog.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> UpdateCatalog(UpdateCatalogDto updateCatalog)
    {
        try
        {
            var catalog = new Catalog()
            {
                Id = updateCatalog.Id,
                CatalogName = updateCatalog.CatalogName
            };
            context.Catalogs.Update(catalog);
            await context.SaveChangesAsync();
            return new Response<int>(catalog.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCatalog(int id)
    {
        try
        {
            var catalog = await context.Catalogs.FindAsync(id);
            if (catalog == null) return new Response<bool>(HttpStatusCode.NotFound, "Catalog not found!");
            context.Catalogs.Remove(catalog);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}