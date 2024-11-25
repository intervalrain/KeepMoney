using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security.Policies;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Users.Queries;

[Authorize(Permissions = Common.Security.Permissions.Permission.User.Get, Policies = Policy.SelfOrAdmin)]
public record GetUserQuery(Guid UserId) : IAuthorizableRequest<ErrorOr<User?>>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User?>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User?>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        return user;
    }
}

