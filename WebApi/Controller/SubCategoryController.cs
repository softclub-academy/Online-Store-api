using System.Net;
using Domain.Dtos.SubCategoryDtos;
using Domain.Response;
using Infrastructure.Services.SubCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class SubCategoryController(ISubCategoryService service) : BaseController
{
    [HttpGet("get-sub-category")]
    public async Task<IActionResult> GetSubCategories()
    {
        var result = await service.GetSubCategories();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-sub-category-by-id")]
    public async Task<IActionResult> GetSubCategoryById(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetSubCategoryById(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetSubCategoryDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-sub-category")]
    public async Task<IActionResult> AddSubCategory(AddSubCategoryDto addSubCategory)
    {
        if (ModelState.IsValid)
        {
            var result = await service.AddSubCategory(addSubCategory);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-sub-category")]
    public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryDto updateSubCategory)
    {
        if (ModelState.IsValid)
        {
            var result = await service.UpdateSubCategory(updateSubCategory);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-sub-category")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteSubCategory(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}