using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Models;

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

    public CurrentUser CurrentUser
    {
        get
        {
            _httpContextAccessor.HttpContext.ThrowIfNull();
            var id = Guid.Parse(GetSingleClaimValue(ClaimTypes.Sid));
            var firstName = GetSingleClaimValue(ClaimTypes.Name);
            var lastName = GetSingleClaimValue(ClaimTypes.GivenName);
            var email = GetSingleClaimValue(ClaimTypes.Email);
            var roles = GetClaimValues(ClaimTypes.Role);
            var permissions = GetClaimValues("permissions");

            return new CurrentUser(id, firstName, lastName, email, roles, permissions);
        }
    }

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;

    private List<string> GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
}

