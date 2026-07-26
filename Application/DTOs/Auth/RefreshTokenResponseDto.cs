using System.Text.Json.Serialization;

namespace Application.DTOs.Auth
{
    public class RefreshTokenResponseDto
    {
        [JsonPropertyName("auth_token")]
        public string AuthToken { get; set; } = string.Empty;
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}