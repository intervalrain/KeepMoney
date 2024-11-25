using KeepMoney.Api.Models;
using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Transactions.Queries;
using KeepMoney.Domain.Transactions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace KeepMoney.Api.Controllers;

public class TransactionController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserProvider _currentUserProvider;

    public TransactionController(IMediator mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
        _currentUserProvider = currentUserProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var currentUser = _currentUserProvider.CurrentUser;
        var query = new GetAllTransactionQuery(currentUser.Id);

        var result = await _mediator.Send(query);

        return result.Match(
            ts => Ok(ts.ConvertAll(TransactionResponse.ToDto)),
            Problem);
    }
}