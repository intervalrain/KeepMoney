using KeepMoney.Domain.Common;
using KeepMoney.Domain.Transactions;

namespace KeepMoney.Domain.Users.Events;

public record TransactionSetEvent(Transaction Transaction) : IDomainEvent;