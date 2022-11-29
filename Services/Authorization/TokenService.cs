using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using TreinamentoMarinho.Entities;

namespace TreinamentoMarinho.Services.Authorization;

public class TokenService
{
    public static string secretKey;

    public static string GenerateToken(UserEntity user)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(secretKey);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.St_login),
                new Claim(ClaimTypes.Role, user.St_role)
            }),
            Expires = DateTime.UtcNow.AddHours(16),
            SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
