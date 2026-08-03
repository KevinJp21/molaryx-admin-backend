using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common;
using Domain.Contracts.IServices;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly JsonWebTokenHandler _tokenHandler = new();

        public string GenerateJwt(
            long idUser,
            short idUserRole,
            long? idTenant,
            string email,
            long idUserSession,
            DateTime expiration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, idUser.ToString()),
                new(ClaimTypes.NameIdentifier, idUser.ToString()),

                new (ClaimTypes.Email, email),
                new(ClaimTypes.Role, idUserRole.ToString()),
                new(AuthClaimTypes.IdUserSession, idUserSession.ToString()),

                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            if (idTenant.HasValue)
            {
                claims.Add(
                    new Claim(
                        AuthClaimTypes.IdTenant,
                        idTenant.Value.ToString()
                    )
                );
            }

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

        public string GenerateToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateResetPasswordToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(randomBytes);
        }

        public string HashRefreshToken(string refreshToken)
        {
            var hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(refreshToken)
            );

            return Convert.ToBase64String(hash);
        }
    }
}