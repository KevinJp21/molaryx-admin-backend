using Application.Common.Mediator.Interfaces;

namespace Application.Features.Patients.Command.CreatePatient
{
    public class CreatePatientCommand : IRequest<bool>
    {
        public short IdIdentificationType { get; set; }
        public string IdentificationNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; } = string.Empty;
        public string? SecondSurname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}