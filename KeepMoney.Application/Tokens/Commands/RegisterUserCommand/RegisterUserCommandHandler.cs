using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Tokens.Commands.RegisterUserCommand;

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