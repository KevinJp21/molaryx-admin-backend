using Application.Common.Mediator.Interfaces;

namespace Application.Features.Users.Command.CreateMember
{
    public class CreateMemberCommand : IRequest<bool>
    {
        public short IdUserRole { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; } = string.Empty;
        public string? SecondSurname { get; set; }
        public short IdIdentificationType { get; set; }
        public string IdentificationNumber { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}