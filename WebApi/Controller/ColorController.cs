﻿using System.Net;
using Domain.Dtos.ColorDtos;
using Domain.Response;
using Infrastructure.Services.ColorService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class ColorController(IColorService service) : BaseController
{
    [HttpGet("get-colors")]
    public async Task<IActionResult> GetColors()
    {
        var result = await service.GetColors();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-color-by-id")]
    public async Task<IActionResult> GetColorById(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetColorById(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetColorDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-color")]
    public async Task<IActionResult> AddColor(AddColorDto addColor)
    {
        if (ModelState.IsValid)
        {
            var result = await service.AddColor(addColor);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-color")]
    public async Task<IActionResult> UpdateColor(UpdateColorDto updateColor)
    {
        if (ModelState.IsValid)
        {
            var result = await service.UpdateColor(updateColor);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-color")]
    public async Task<IActionResult> DeleteColor(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteColor(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}