using System.Net;
using Domain.Dtos.CatalogDtos;
using Domain.Response;
using Infrastructure.Services.CatalogService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class CatalogController(ICatalogService service) : BaseController
{
    [HttpGet("get-catalogs")]
    public async Task<IActionResult> GetCatalogs()
    {
        var result = await service.GetCatalogs();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-catalog-by-id")]
    public async Task<IActionResult> GetCatalogById(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetCatalogById(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetCatalogDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-catalog")]
    public async Task<IActionResult> AddCatalog(AddCatalogDto addCatalog)
    {
        if (ModelState.IsValid)
        {
            var result = await service.AddCatalog(addCatalog);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-catalog")]
    public async Task<IActionResult> Update(UpdateCatalogDto updateCatalog)
    {
        if (ModelState.IsValid)
        {
            var result = await service.UpdateCatalog(updateCatalog);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}