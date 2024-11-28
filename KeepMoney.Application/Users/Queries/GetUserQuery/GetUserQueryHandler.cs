using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Users.Queries.GetUserQuery;

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

