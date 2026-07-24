namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserResponse
    {
        public short IdUserRole { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string FirstSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}