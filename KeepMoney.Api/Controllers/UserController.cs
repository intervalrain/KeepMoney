using KeepMoney.Api.Models;
using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Users.Queries;
using KeepMoney.Application.Users.Queries.GetUserQuery;
using KeepMoney.Application.Users.Queries.GetUsersQuery;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace KeepMoney.Api.Controllers;

public class UserController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserProvider _currentUserProvider;

    public UserController(IMediator mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
        _currentUserProvider = currentUserProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAcount()
    {
        var query = new GetUserQuery(_currentUserProvider.CurrentUser.Id);
        var result = await _mediator.Send(query);
        return result.Match(
            user => Ok(UserResponse.ToDto(user)),
            Problem);
    }

    [HttpGet("get_all_users")]
    public async Task<IActionResult> GetAcounts()
    {
        var query = new GetUsersQuery(_currentUserProvider.CurrentUser.Id);
        var result = await _mediator.Send(query);
        return result.Match(
            users => Ok(users.ConvertAll(UserResponse.ToDto)),
            Problem);
    }

    [HttpPut]
    public Task<IActionResult> UpdateAcount(string firstName, string lastName, string email, string password)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public Task<IActionResult> DeleteAcount(Guid userId)
    {
        throw new NotImplementedException();
    }
}