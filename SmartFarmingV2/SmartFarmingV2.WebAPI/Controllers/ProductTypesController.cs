using Microsoft.AspNetCore.Mvc;
using SmartFarmingV2.Entities.DTOs;
//using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Business.Services;

namespace SmartFarmingV2.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductTypesController(IProductTypeService productTypeService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = productTypeService.GetAll();
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateProductTypeDto request)
    {
        string message = productTypeService.Create(request);
        return Ok(new { Message = message });
    }
    [HttpPost]
    public IActionResult Update(UpdateProductTypeDto request)
    {
        string message = productTypeService.Update(request);
        return Ok(new { Message = message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        string message = productTypeService.DeleteById(id);
        return Ok(new { Message = message });
    }
}
