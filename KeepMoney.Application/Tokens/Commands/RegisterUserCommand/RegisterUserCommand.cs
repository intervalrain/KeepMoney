using ErrorOr;

using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Tokens.Commands.RegisterUserCommand;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<User?>>;