using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Tokens.Queries.GenerateTokenQuery;

public record GenerateTokenResult(string Token, string Email, string FirstName, string LastName, SubscriptionType SubscriptionType);