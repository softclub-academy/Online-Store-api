using System.Net;
using Domain.Dtos.BrandDtos;
using Domain.Dtos.ColorDtos;
using Domain.Dtos.ProductDtos;
using Domain.Dtos.SmartphoneDtos;
using Domain.Dtos.UserProfileDtos;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductService;

public class ProductService(ApplicationContext context, IFileService fileService) : IProductService
{
    public async Task<Response<GetProductPageDto>> GetProductPage(ProductFilter filter)
    {
        try
        {
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter.ProductName))
                products = products.Where(p => p.ProductName.ToLower().Contains(filter.ProductName));
            if (!string.IsNullOrEmpty(filter.UserId))
                products = products.Where(p => p.UserId == filter.UserId);
            if (filter.MinPrice != 0 && filter.MaxPrice != 0)
                products = products.Where(p => p.Price >= filter.MinPrice && p.Price <= filter.MaxPrice);
            if (filter.BrandId != 0)
                products = products.Where(p => p.BrandId == filter.BrandId);
            if (filter.CategoryId != 0)
                products = products.Where(p => p.SubCategory.CategoryId == filter.CategoryId);
            if (filter.SubcategoryId != 0)
                products = products.Where(p => p.SubCategoryId == filter.SubcategoryId);
            if (filter.ColorId != 0)
                products = products.Where(p => p.ColorId == filter.ColorId);
            var rangePrice = new GetMinMaxPriceDto()
            {
                MinPrice = await products.MinAsync(p => p.Price),
                MaxPrice = await products.MaxAsync(p => p.Price)
            };
            var colors = await (from p in products
                select new GetColorDto()
                {
                    Id = p.Color.Id,
                    ColorName = p.Color.ColorName
                }).ToListAsync();
            var brands = await (from p in products
                select new GetBrandDto()
                {
                    Id = p.Brand.Id,
                    BrandName = p.Brand.BrandName
                }).ToListAsync();
            var allProducts = await (from p in products
                select new GetProductsDto()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Image = p.ProductImages.Select(i => i.ImageName).FirstOrDefault()!,
                    Price = p.Price,
                    DiscountPrice = p.DiscountPrice
                }).ToListAsync();
            // var brands = await brandService.GetBrands(filter.BrandFilter);
            // var colors = await colorService.GetColors(filter.ColorFilter);
            var result = new GetProductPageDto()
            {
                Products = allProducts,
                MinMaxPrice = rangePrice,
                Brands = brands,
                Colors = colors
            };
            return new Response<GetProductPageDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetProductPageDto>(HttpStatusCode.InternalServerError, e.Message);
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
                GetSmartphone = p.Smartphone != null
                    ? new GetSmartphoneDto()
                    {
                        Id = p.Smartphone.Id,
                        Model = p.Smartphone.Model,
                        Os = p.Smartphone.Os,
                        Communication = p.Smartphone.Communication,
                        Processor = p.Smartphone.Processor,
                        ProcessorFrequency = p.Smartphone.ProcessorFrequency,
                        NumberOfCores = p.Smartphone.NumberOfCores,
                        VideoProcessor = p.Smartphone.VideoProcessor,
                        AspectRatio = p.Smartphone.AspectRatio,
                        DisplayType = p.Smartphone.DisplayType,
                        DisplayResolution = p.Smartphone.DisplayResolution,
                        PixelPerInch = p.Smartphone.PixelPerInch,
                        ScreenRefreshRate = p.Smartphone.ScreenRefreshRate,
                        Diagonal = p.Smartphone.Diagonal,
                        SimCard = p.Smartphone.SimCard,
                        Ram = p.Smartphone.Ram,
                        Rom = p.Smartphone.Rom
                    }
                    : null,
                Users = p.User.Products.Where(u => u.Code == p.Code).Select(pr => new GetUserShortInfoDto()
                {
                    UserId = pr.User.Id,
                    UserName = pr.User.UserName!,
                    FullName = pr.User.UserProfile.FirstName + " " + pr.User.UserProfile.FirstName,
                    ImageName = pr.User.UserProfile.Image
                }).ToList()
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