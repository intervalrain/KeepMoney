using ErrorOr;
using MediatR;

namespace KeepMoney.Application.Tokens.Queries.GenerateTokenQuery;

public record GenerateTokenQuery(string Email, string Password) : IRequest<ErrorOr<GenerateTokenResult>>;