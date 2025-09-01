using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Application.Commands.ServiceCategory;
using ServiceHub.Application.DTOS.ServiceCategoryDTOS;
using ServiceHub.Application.Queries.ServiceCategories;

namespace ServiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceCategoryController : ControllerBase
{
    private readonly ILogger<ServiceCategoryController> _logger;
    private readonly IMediator _mediator;
    public ServiceCategoryController(ILogger<ServiceCategoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServiceCategories()
    {
        var response = await _mediator.Send(new GetAllServiceCategoriesQuery());
        return StatusCode(response.StatusCode,response);
    }


    [HttpPost]
    public async Task<IActionResult> CreateServiceCategory(CreateServiceCategoryCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode,response);
    }
}
