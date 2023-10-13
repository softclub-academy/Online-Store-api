using System.Net;
using Domain.Dtos.ProductDtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductService;

public class ProductService(ApplicationContext context) : IProductService
{
    public async Task<Response<List<GetProductDto>>> GetProducts()
    {
        try
        {
            var products = await context.Products.Select(p => new GetProductDto()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                Brand = p.Brand!.BrandName,
                Color = p.Color!.ColorName,
                Size = p.Size,
                Weight = p.Weight
            }).ToListAsync();
            return new Response<List<GetProductDto>>(products);
        }
        catch (Exception e)
        {
            return new Response<List<GetProductDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetProductDto>> GetProductById(int id)
    {
        try
        {
            var product = await context.Products.Select(p => new GetProductDto()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                Brand = p.Brand!.BrandName,
                Color = p.Color!.ColorName,
                Size = p.Size,
                Weight = p.Weight
            }).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found");
            return new Response<GetProductDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetProductDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddProduct(AddProductDto addProduct)
    {
        try
        {
            var product = new Product()
            {
                UserId = addProduct.UserId,
                ProductName = addProduct.ProductName,
                Description = addProduct.Description,
                BrandId = addProduct.BrandId,
                ColorId = addProduct.ColorId,
                Weight = addProduct.Weight,
                Size = addProduct.Size
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return new Response<int>(product.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct)
    {
        try
        {
            var product = new Product()
            {
                Id = updateProduct.Id,
                UserId = updateProduct.UserId,
                ProductName = updateProduct.ProductName,
                Description = updateProduct.Description,
                BrandId = updateProduct.BrandId,
                ColorId = updateProduct.ColorId,
                Weight = updateProduct.Weight,
                Size = updateProduct.Size
            };
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return new Response<int>(product.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteProduct(int id)
    {
        try
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return new Response<bool>(HttpStatusCode.NotFound, "Product not found!");
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}