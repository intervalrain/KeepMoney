using ErrorOr;
using KeepMoney.Application.Common.Security.Policies;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Domain.Transactions;

namespace KeepMoney.Application.Transactions.Queries;

[Authorize(Permissions = Common.Security.Permissions.Permission.Transaction.GetAll, Policies = Policy.SelfOrAdmin)]
public record GetAllTransactionsQuery(Guid UserId) : IAuthorizableRequest<ErrorOr<List<Transaction>>>;