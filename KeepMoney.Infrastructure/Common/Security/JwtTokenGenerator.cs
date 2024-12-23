﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Models;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KeepMoney.Infrastructure.Common.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string email, string firstName, string lastName, List<string> roles, List<string> permissions)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, firstName),
            new(ClaimTypes.GivenName, lastName),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Sid, userId.ToString()),
        };
        roles.ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
        permissions.ForEach(permission => claims.Add(new("permissions", permission)));

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

