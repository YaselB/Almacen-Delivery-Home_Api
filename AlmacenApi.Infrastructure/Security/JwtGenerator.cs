using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlmacenApi.Aplication.Common.Interfaces.Jwt;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlmacenApi.Infrastructure.Security;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings settings;
    public JwtGenerator(IOptions<JwtSettings> options)
    {
        settings = options.Value;
    }
    public string GenerateToken(AdminEntity admin)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , admin.id),
            new Claim(JwtRegisteredClaimNames.UniqueName , admin.Username),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            new Claim("tipo" , "admin")
        };
        foreach (var permission in admin.Permission)
    {
        
        claims.Add(new Claim("permission", permission));
    }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
        var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes),
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateUserToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , user.id),
            new Claim(JwtRegisteredClaimNames.UniqueName , user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            new Claim("tipo" , "admin")
        };
        foreach(var permission in user.Permission)
        {
            Console.WriteLine(permission);
            claims.Add(new Claim("permission" , permission));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
        var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes),
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}