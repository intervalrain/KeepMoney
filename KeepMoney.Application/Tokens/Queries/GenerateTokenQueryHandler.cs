using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Tokens.Queries;

public record GenerateTokenQuery(string Email, string Password) : IRequest<ErrorOr<GenerateTokenResult>>;

public record GenerateTokenResult(string Token, string Email, string FirstName, string LastName, SubscriptionType SubscriptionType);

public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, ErrorOr<GenerateTokenResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public GenerateTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<GenerateTokenResult>> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null) return Error.NotFound("User.NotFound", "User not found with the given email.");
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash)) return Error.Validation("Auth.InvalidCredentials", "Invalid credentials");

        var token = _jwtTokenGenerator.GenerateToken(
            userId: user.Id,
            email: user.Email,
            firstName: user.FirstName,
            lastName: user.LastName,
            subscriptionType: user.SubscriptionType,
            role: user.Role
        );

        var authResult = new GenerateTokenResult(token, user.Email, user.FirstName, user.LastName, user.SubscriptionType);

        return authResult;
    }
}