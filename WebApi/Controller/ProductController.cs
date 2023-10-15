using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.ProductDtos;
using Domain.Response;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class ProductController(IProductService service) : BaseController
{
    [HttpGet("get-products"), AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        var result = await service.GetProducts();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-product-by-id"), AllowAnonymous]
    public async Task<IActionResult> GetProductById([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetProductById(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetProductDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(AddProductDto addProduct)
    {
        if (ModelState.IsValid)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await service.AddProduct(addProduct, user);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-product")]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProduct)
    {
        if (ModelState.IsValid)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await service.UpdateProduct(updateProduct, user);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("delete-product")]
    public async Task<IActionResult> DeleteProduct([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteProduct(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}