using System.Text.Json.Serialization;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserResponse
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("first_surname")]
        public string FirstSurname { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}