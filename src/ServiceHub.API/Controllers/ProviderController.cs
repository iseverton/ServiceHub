using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Application.Commands.Providers;

namespace ServiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProviderController : ControllerBase
{
    private readonly ILogger<ProviderController> _logger;
    private readonly IMediator _mediator;
    public ProviderController(ILogger<ProviderController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("add-available-time")]
    public async Task<IActionResult> AddAvailableTime(AddProviderAvailableTimeCommand command,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to add available time for provider {ProviderId}", command.ProviderId);
        var result = await _mediator.Send(command,cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
