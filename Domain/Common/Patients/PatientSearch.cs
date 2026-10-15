using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Common.Patients
{
    public static class PatientSearch
    {
        public static Expression<Func<Patient, bool>> MatchesToken(string token)
        {
            var normalized = token.ToLowerInvariant();

            return Search.ClassifyToken(token) switch
            {
                SearchTermKind.Email =>
                    patient => patient.Email.ToLower().Contains(normalized),

                SearchTermKind.Numeric =>
                    patient =>
                        patient.PhoneNumber.Contains(token) ||
                        patient.IdentificationNumber.ToLower().Contains(normalized),

                SearchTermKind.Identification =>
                    patient => patient.IdentificationNumber.ToLower().Contains(normalized),

                _ => patient =>
                    patient.FirstName.ToLower().Contains(normalized) ||
                    (patient.SecondName != null && patient.SecondName.ToLower().Contains(normalized)) ||
                    patient.FirstSurname.ToLower().Contains(normalized) ||
                    (patient.SecondSurname != null && patient.SecondSurname.ToLower().Contains(normalized))
            };
        }
    }
}
