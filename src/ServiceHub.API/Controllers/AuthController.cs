using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Commands.Providers;
using ServiceHub.Application.Commands.Users;
using System.Threading.Tasks;

namespace ServiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("register-provider")]
    public async Task<IActionResult> RegisterProvider(RegisterProviderCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}
