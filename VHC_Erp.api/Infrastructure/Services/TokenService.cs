using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Infrastructure.Interfaces;

namespace VHC_Erp.api.Infrastructure.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    private readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]!));
    public string CreateToken(UserIdentity users)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, users.Email!),
            new Claim(JwtRegisteredClaimNames.GivenName, users.UserName!)
        };
        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}