namespace Application.Common.Interfaces
{
    public interface IOwnerRegistration
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string? SecondName { get; set; }

        public string FirstSurname { get; set; }

        public string? SecondSurname { get; set; }

        public short IdIdentificationType { get; set; }

        public string IdentificationNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}