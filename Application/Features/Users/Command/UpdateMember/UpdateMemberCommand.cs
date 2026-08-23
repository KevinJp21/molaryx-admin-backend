using Application.Common.Mediator.Interfaces;

namespace Application.Features.Users.Command.UpdateMember
{
    public class UpdateMemberCommand : IRequest<bool>
    {
        public long IdUser { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FirstSurname { get; set; }
        public string? SecondSurname { get; set; }
        public short? IdIdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
