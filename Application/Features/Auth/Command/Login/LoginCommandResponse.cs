namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandResponse
    {
        public string AuthToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}