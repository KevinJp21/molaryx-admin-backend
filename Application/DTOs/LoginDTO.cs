using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class LoginDTO
    {
        [JsonPropertyName("auth_token")]
        public string AuthToken { get; set; } = string.Empty;
    }
}