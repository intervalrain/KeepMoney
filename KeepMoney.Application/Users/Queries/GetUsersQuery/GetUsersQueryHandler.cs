using ErrorOr;

using KeepMoney.Application.Common.Persistence;
using KeepMoney.Domain.Users;

using MediatR;

namespace KeepMoney.Application.Users.Queries.GetUsersQuery;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAsync(cancellationToken: cancellationToken);
        return users.ToList();
    }
}

