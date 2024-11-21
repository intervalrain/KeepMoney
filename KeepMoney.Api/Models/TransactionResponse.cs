namespace KeepMoney.Api.Models;

public record TransactionResponse(DateTime Date, string Category, decimal Amount, string Note);