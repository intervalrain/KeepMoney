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

    [HttpPost]
    public async Task<IActionResult> GenerateToken(GenerateTokenQuery request)
    {
        var result = await _mediator.Send(request);
        return result.Match(Ok, Problem);
    }
}
