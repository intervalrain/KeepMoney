using ErrorOr;

using FluentValidation;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Tokens.Commands;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<User?>>;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(32);
        RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(32);
        RuleFor(x => x.Email).EmailAddress();
    }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<User?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<User?>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
        {
            return Error.Conflict("User.Conflict", "The email has been registered.");
        }
        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            Subscription.Create(SubscriptionType.Basic, DateTime.UtcNow),
            new List<string>(),
            _passwordHasher.Hash(command.Password));

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return user;
    }
}