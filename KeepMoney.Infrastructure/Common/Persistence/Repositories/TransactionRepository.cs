using KeepMoney.Application.Common.Persistence;
using KeepMoney.Domain.Transactions;
using KeepMoney.Infrastructure.Common.Data;

namespace KeepMoney.Infrastructure.Common.Persistence.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context)
        : base(context)
    {
    }
}

