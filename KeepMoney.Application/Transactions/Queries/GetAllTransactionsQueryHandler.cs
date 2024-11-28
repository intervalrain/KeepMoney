using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Domain.Transactions;

using MediatR;

namespace KeepMoney.Application.Transactions.Queries;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, ErrorOr<List<Transaction>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<List<Transaction>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        return (await _transactionRepository.GetAsync(t => t.UserId == request.UserId, cancellationToken: cancellationToken)).ToList();
    }
}

