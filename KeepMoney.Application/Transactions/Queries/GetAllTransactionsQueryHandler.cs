using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security.Policies;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Domain.Transactions;

using MediatR;

namespace KeepMoney.Application.Transactions.Queries;

[Authorize(Permissions = Common.Security.Permissions.Permission.Transaction.GetAll, Policies = Policy.SelfOrAdmin)]
public record GetAllTransactionQuery(Guid UserId, Guid SubscriptionId) : IAuthorizableRequest<ErrorOr<List<Transaction>>>;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionQuery, ErrorOr<List<Transaction>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<List<Transaction>>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
    {
        return (await _transactionRepository.GetAsync(t => t.UserId == request.UserId, cancellationToken: cancellationToken)).ToList();
    }
}

