using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.BrandDtos;
using Domain.Response;
using Infrastructure.Services.BrandService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class BrandController(IBrandService service) : BaseController
{
    [HttpGet("get-brands")]
    public async Task<IActionResult> GetBrands()
    {
        var response = await service.GetBrands();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("get-brand-by-id")]
    public async Task<IActionResult> GetBrandById([Required]int id)
    {
        var response = await service.GetBrandById(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-brand")]
    public async Task<IActionResult> AddBrand(AddBrandDto addBrand)
    {
        if (ModelState.IsValid)
        {
            var result = await service.AddBrand(addBrand);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-brand")]
    public async Task<IActionResult> Update(UpdateBrandDto updateBrand)
    {
        if (ModelState.IsValid)
        {
            var result = await service.UpdateBrand(updateBrand);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-brand")]
    public async Task<IActionResult> DeleteBrand([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteBrand(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}