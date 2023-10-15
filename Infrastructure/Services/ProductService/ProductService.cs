using System.Net;
using Domain.Dtos.ProductDtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductService;

public class ProductService(ApplicationContext context, IFileService fileService) : IProductService
{
    public async Task<Response<List<GetProductsDto>>> GetProducts()
    {
        try
        {
            var products = await context.Products.Select(p => new GetProductsDto()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Image = p.ProductImages.Select(i => i.ImageName).FirstOrDefault()!,
                Price = p.Price
            }).ToListAsync();
            return new Response<List<GetProductsDto>>(products);
        }
        catch (Exception e)
        {
            return new Response<List<GetProductsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetProductDto>> GetProductById(int id)
    {
        try
        {
            var product = await context.Products.Select(p => new GetProductDto()
            {
                
                Id = p.Id,
                SubCategoryId = p.SubCategoryId,
                ProductName = p.ProductName,
                Description = p.Description,
                Brand = p.Brand.BrandName,
                Color = p.Color.ColorName,
                Size = p.Size,
                Weight = p.Weight,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                Code = p.Code,
                Images = p.ProductImages.Select(i => i.ImageName).ToList(),
            }).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found");
            return new Response<GetProductDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetProductDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddProduct(AddProductDto addProduct, string user)
    {
        try
        {
            var product = new Product()
            {
                UserId = user,
                ProductName = addProduct.ProductName,
                Description = addProduct.Description,
                Code = addProduct.Code,
                BrandId = addProduct.BrandId,
                ColorId = addProduct.ColorId,
                Weight = addProduct.Weight,
                Size = addProduct.Size,
                SubCategoryId = addProduct.SubCategoryId,
                Price = addProduct.Price,
                DiscountPrice = addProduct.DiscountPrice,
                Quantity = addProduct.Quantity
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            foreach (var file in addProduct.Images)
            {
                var imageName = fileService.CreateFile(file);
                var image = new ProductImage()
                {
                    ProductId = product.Id,
                    ImageName = imageName.Data!
                };
                var list = new List<string>();
                await context.ProductImages.AddAsync(image);
                await context.SaveChangesAsync();
            }
            return new Response<int>(product.Id);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> UpdateProduct(UpdateProductDto updateProduct, string user)
    {
        try
        {
            var product = new Product()
            {
                Id = updateProduct.Id,
                UserId = user,
                ProductName = updateProduct.ProductName,
                Description = updateProduct.Description,
                BrandId = updateProduct.BrandId,
                ColorId = updateProduct.ColorId,
                Weight = updateProduct.Weight,
                Size = updateProduct.Size,
                SubCategoryId = updateProduct.SubCategoryId,
                Price = updateProduct.Price,
                DiscountPrice = updateProduct.DiscountPrice,
                Quantity = updateProduct.Quantity
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