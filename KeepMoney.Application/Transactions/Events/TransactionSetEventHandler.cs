using KeepMoney.Application.Common.Persistence;
using KeepMoney.Domain.Users.Events;

using MediatR;

namespace KeepMoney.Application.Transactions.Events;

public class TransactionSetEventHandler : INotificationHandler<TransactionSetEvent>
{
    private readonly ITransactionRepository _repo;

    public TransactionSetEventHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(TransactionSetEvent notification, CancellationToken cancellationToken)
    {
        await _repo.AddAsync(notification.Transaction, cancellationToken);
    }
}

