using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Column("id_user")]
        public long IdUser { get; set; }
        [Column("id_user_status")]
        public short IdUserStatus { get; set; }
        [Column("id_user_rol")]
        public short IdUserRole { get; set; }
        [Column("id_tenant")]
        public long? IdTenant { get; set; }
        [Column("username")]
        public string Username { get; set; } = string.Empty;
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("second_name")]
        public string? SecondName { get; set; }
        [Column("first_surname")]
        public string FirstSurname { get; set; } = string.Empty;
        [Column("second_surname")]
        public string? SecondSurname { get; set; }
        [Column("id_identification_type")]
        public short IdIdentificationType { get; set; }
        [Column("identification_number")]
        public string IdentificationNumber { get; set; } = string.Empty;
        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public byte[] Password { get; set; } = null!;
        [Column("salt")]
        public byte[] Salt { get; set; } = null!;

        // Navigation properties
        public UserStatus UserStatus { get; set; } = null!;
        public UserRole UserRole { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
        public IdentificationType IdentificationType { get; set; } = null!;
        public ICollection<UserSession> UserSessions { get; set; } = [];
        public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = [];
    }
}