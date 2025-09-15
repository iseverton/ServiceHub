using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.API.Extensions;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Commands.Providers;
using ServiceHub.Application.Commands.Users;

namespace ServiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;
    public AuthController(ILogger<AuthController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command, IValidator<RegisterUserCommand> validator)
    {
        _logger.LogInformation("Received RegisterUser request for email: {@Email}", command.Email);

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for RegisterUser request: {@Errors}", validationResult.Errors);
            validationResult.AddToModelState(ModelState);
            return UnprocessableEntity(ModelState);
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("register-provider")]
    public async Task<IActionResult> RegisterProvider(RegisterProviderCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("emailconfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] string token, [FromQuery] string userId, CancellationToken cancellationToken)
    {
        var command = new EmailConfirmationCommand(token, userId);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }
}
