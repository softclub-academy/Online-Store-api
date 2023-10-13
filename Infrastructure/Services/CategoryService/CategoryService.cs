using System.Net;
using Domain.Dtos.CategoryDtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CategoryService;

public class CategoryService(ApplicationContext context) : ICategoryService
{
    public async Task<Response<List<GetCategoryDto>>> GetCategories()
    {
        try
        {
            var categories = await context.Categories.Select(c => new GetCategoryDto()
            {
                Id = c.Id,
                CatalogId = c.CatalogId,
                CategoryName = c.CategoryName
            }).ToListAsync();
            return new Response<List<GetCategoryDto>>(categories);
        }
        catch (Exception e)
        {
            return new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCategoryDto>> GetCategoryById(int id)
    {
        try
        {
            var category = await context.Categories.Select(c => new GetCategoryDto()
            {
                Id = c.Id,
                CatalogId = c.CatalogId,
                CategoryName = c.CategoryName
            }).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not found!");
            return new Response<GetCategoryDto>(category);
        }
        catch (Exception e)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddCategory(AddCategoryDto addCategory)
    {
        try
        {
            var category = new Category()
            {
                CatalogId = addCategory.CatalogId,
                CategoryName = addCategory.CategoryName
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return new Response<int>(category.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> UpdateCategory(UpdateCategoryDto updateCategory)
    {
        try
        {
            var category = new Category()
            {
                Id = updateCategory.Id,
                CatalogId = updateCategory.CatalogId,
                CategoryName = updateCategory.CategoryName
            };
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<int>(category.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCategory(int id)
    {
        try
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return new Response<bool>(HttpStatusCode.BadRequest, "Category not found!");
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}