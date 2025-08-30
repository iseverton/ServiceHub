using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}
