using KeepMoney.Application.Common.Security.Models;

namespace KeepMoney.Application.Common.Security;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser { get; }
}

