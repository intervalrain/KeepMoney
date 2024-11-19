using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Models;
using KeepMoney.Domain.Users;

using Microsoft.AspNetCore.Http;

using Throw;

namespace KeepMoney.Infrastructure.Common.Security;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser GetCurrentUser
    {
        get
        {
            _httpContextAccessor.HttpContext.ThrowIfNull();
            var id = Guid.Parse(GetSingleClaimValue("id"));
            var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
            var lastName = GetSingleClaimValue(JwtRegisteredClaimNames.FamilyName);
            var email = GetSingleClaimValue(JwtRegisteredClaimNames.Email);
            var role = GetSingleClaimValue(ClaimTypes.Role);

            return new CurrentUser(id, firstName, lastName, email, role == "admin" ? Role.Admin : role == "dev" ? Role.Dev : Role.User);
        }
    }

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}

