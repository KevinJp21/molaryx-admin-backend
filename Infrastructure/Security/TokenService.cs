using System.Security.Claims;
using System.Text;
using Domain.Contracts.IServices;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly JsonWebTokenHandler _tokenHandler = new();

        public string GenerateToken(
            long idUser,
            short idUserRole,
            string email,
            DateTime expiration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, idUser.ToString()),
                new(ClaimTypes.NameIdentifier, idUser.ToString()),

                new (ClaimTypes.Email, email),

                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            return _tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}