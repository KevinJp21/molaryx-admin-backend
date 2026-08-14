using Application.Common.Mediator.Interfaces;

namespace Application.Features.Patients.Command.UpdatePatient
{
    public class UpdatePatientCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public short? IdIdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FirstSurname { get; set; }
        public string? SecondSurname { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
    }
}