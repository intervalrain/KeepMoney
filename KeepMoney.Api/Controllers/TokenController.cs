using KeepMoney.Api.Models;
using KeepMoney.Application.Tokens.Commands;
using KeepMoney.Application.Tokens.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepMoney.Api.Controllers;

[AllowAnonymous]
public class TokenController : ApiController
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register_acount")]
    public async Task<IActionResult> RegisterAcount(string firstName, string lastName, string email, string password)
    {
        var command = new RegisterUserCommand(firstName, lastName, email, password);
        var result = await _mediator.Send(command);
        return result.Match(
            user => Ok(UserResponse.ToDto(user)),
            Problem);
    }

    [HttpPost("generate_token")]
    public async Task<IActionResult> GenerateToken(GenerateTokenQuery request)
    {
        var result = await _mediator.Send(request);
        return result.Match(Ok, Problem);
    }
}
