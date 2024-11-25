using KeepMoney.Domain.Transactions;

namespace KeepMoney.Api.Models;

public record TransactionResponse(DateTime Date, string Category, decimal Amount, string Note)
{
    public static TransactionResponse ToDto(Transaction t) => new(t.Date, t.Category, t.Amount, t.Note);
}