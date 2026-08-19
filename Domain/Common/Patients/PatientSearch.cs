using System.Linq.Expressions;
using Domain.Common;
using Domain.Entities;

namespace Domain.Common.Patients
{
    public static class PatientSearch
    {
        public const int MinTokenLength = 3;

        public static string[] GetTokens(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return [];
            }

            return [.. SearchText.Tokens(search)
                .Where(token => token.Length >= MinTokenLength)];
        }

        public static bool HasValidSearch(string? search)
            => GetTokens(search).Length > 0;

        public static PatientSearchTermKind ClassifyToken(string token)
        {
            if (token.Contains('@'))
            {
                return PatientSearchTermKind.Email;
            }

            if (token.All(char.IsDigit))
            {
                return PatientSearchTermKind.Numeric;
            }

            if (token.Any(char.IsDigit) && token.Any(char.IsLetter))
            {
                return PatientSearchTermKind.Identification;
            }

            return PatientSearchTermKind.Name;
        }

        public static Expression<Func<Patient, bool>> MatchesToken(string token)
        {
            var normalized = token.ToLowerInvariant();

            return ClassifyToken(token) switch
            {
                PatientSearchTermKind.Email =>
                    patient => patient.Email.ToLower().Contains(normalized),

                PatientSearchTermKind.Numeric =>
                    patient =>
                        patient.PhoneNumber.Contains(token) ||
                        patient.IdentificationNumber.ToLower().Contains(normalized),

                PatientSearchTermKind.Identification =>
                    patient => patient.IdentificationNumber.ToLower().Contains(normalized),

                _ => patient =>
                    patient.FirstName.ToLower().Contains(normalized) ||
                    (patient.SecondName != null && patient.SecondName.ToLower().Contains(normalized)) ||
                    patient.FirstSurname.ToLower().Contains(normalized) ||
                    (patient.SecondSurname != null && patient.SecondSurname.ToLower().Contains(normalized))
            };
        }

        public static Expression<Func<PatientTreatment, bool>> MatchesPatientTreatmentToken(
            string token)
        {
            var normalized = token.ToLowerInvariant();

            return ClassifyToken(token) switch
            {
                PatientSearchTermKind.Email =>
                    patientTreatment =>
                        patientTreatment.Patient.Email.ToLower().Contains(normalized),

                PatientSearchTermKind.Numeric =>
                    patientTreatment =>
                        patientTreatment.Patient.PhoneNumber.Contains(token) ||
                        patientTreatment.Patient.IdentificationNumber.ToLower().Contains(normalized),

                PatientSearchTermKind.Identification =>
                    patientTreatment =>
                        patientTreatment.Patient.IdentificationNumber.ToLower().Contains(normalized),

                _ => patientTreatment =>
                    patientTreatment.Treatment.Name.ToLower().Contains(normalized) ||
                    patientTreatment.Patient.FirstName.ToLower().Contains(normalized) ||
                    (patientTreatment.Patient.SecondName != null &&
                     patientTreatment.Patient.SecondName.ToLower().Contains(normalized)) ||
                    patientTreatment.Patient.FirstSurname.ToLower().Contains(normalized) ||
                    (patientTreatment.Patient.SecondSurname != null &&
                     patientTreatment.Patient.SecondSurname.ToLower().Contains(normalized))
            };
        }
    }
}
