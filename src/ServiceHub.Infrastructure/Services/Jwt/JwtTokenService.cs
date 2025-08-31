using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Services.Jwt;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<JwtTokenService> _logger;
    public JwtTokenService(IOptions<JwtSettings> jwtSettings, ILogger<JwtTokenService> logger)
    {
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public string GenerateToken(ApplicationUser applicationUser)
    {
        _logger.LogInformation("Starting JWT token generation for user ID: {UserId}", applicationUser.Id);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,applicationUser.Id.ToString()),
            new Claim(ClaimTypes.Email,applicationUser.Email)
        };

        // security
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var credentials = new SigningCredentials
        (
           new SymmetricSecurityKey(key),
           SecurityAlgorithms.HmacSha256Signature
        );

        // configuretion token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            SigningCredentials = credentials
        };

        // create token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        _logger.LogInformation("JWT Token generated for user {UserId} at {Time}", applicationUser.Id, DateTime.UtcNow);
        
        return tokenHandler.WriteToken(token);
    }
}
