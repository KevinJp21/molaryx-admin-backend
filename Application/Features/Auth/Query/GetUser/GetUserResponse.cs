using System.Text.Json.Serialization;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserResponse
    {
        public string FirstName { get; set; } = null!;
        public string FirstSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}