using System.Net;
using Domain.Dtos.BrandDtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Infrastructure.Services.BrandService;

public class BrandService(ApplicationContext context) : IBrandService
{
    public async Task<Response<List<GetBrandDto>>> GetBrands()
    {
        try
        {
            var brands = await context.Brands.Select(b => new GetBrandDto()
            {
                Id = b.Id,
                BrandName = b.BrandName
            }).ToListAsync();
            return new Response<List<GetBrandDto>>(brands);
        }
        catch (Exception e)
        {
            return new Response<List<GetBrandDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetBrandDto>> GetBrandById(int id)
    {
        try
        {
            var brand = await context.Brands.Select(b => new GetBrandDto()
            {
                Id = b.Id,
                BrandName = b.BrandName
            }).FirstOrDefaultAsync(b => b.Id == id);
            if (brand == null) return new Response<GetBrandDto>(HttpStatusCode.BadRequest, "Brand not found!");
            return new Response<GetBrandDto>(brand);
        }
        catch (Exception e)
        {
            return new Response<GetBrandDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddBrand(AddBrandDto addBrand)
    {
        try
        {
            var brand = new Brand()
            {
                BrandName = addBrand.BrandName
            };
            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync();
            return new Response<int>(brand.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> UpdateBrand(UpdateBrandDto updateBrand)
    {
        try
        {
            var brand = new Brand()
            {
                Id = updateBrand.Id,
                BrandName = updateBrand.BrandName
            };
            context.Brands.Update(brand);
            await context.SaveChangesAsync();
            return new Response<int>(brand.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteBrand(int id)
    {
        try
        {
            var brand = await context.Brands.FindAsync(id);
            if (brand == null) return new Response<bool>(HttpStatusCode.BadRequest, "Brand not found!");
            context.Brands.Remove(brand);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}