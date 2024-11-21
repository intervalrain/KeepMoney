using MediatR;

namespace KeepMoney.Application.Common.Security.Request;

public interface IAuthorizableRequest<T> : IRequest<T>
{
    Guid UserId { get; }
}

