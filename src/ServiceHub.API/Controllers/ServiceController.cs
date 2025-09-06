using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Application.Commands.Services;

namespace ServiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly IMediator _mediator;
    public ServiceController(ILogger<ServiceController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateService(CreateServiceCommand command,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new service with title: {@Title}", command.Title);
        var result = await _mediator.Send(command,cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
