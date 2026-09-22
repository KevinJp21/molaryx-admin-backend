using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Common.Patients
{
    public static class PatientTreatmentSearch
    {
        public const int MinTokenLength = PatientSearch.MinTokenLength;

        public static string[] GetTokens(string? search)
            => PatientSearch.GetTokens(search);

        public static bool HasValidSearch(string? search)
            => PatientSearch.HasValidSearch(search);

        public static Expression<Func<PatientTreatment, bool>> MatchesToken(string token)
        {
            var matchesPatient = Predicate.For<PatientTreatment, Patient>(
                patientTreatment => patientTreatment.Patient,
                PatientSearch.MatchesToken(token));

            if (PatientSearch.ClassifyToken(token) != PatientSearchTermKind.Name)
            {
                return matchesPatient;
            }

            var normalized = token.ToLowerInvariant();
            Expression<Func<PatientTreatment, bool>> matchesTreatment =
                patientTreatment => patientTreatment.Treatment.Name.ToLower().Contains(normalized);

            return Predicate.Or(matchesPatient, matchesTreatment);
        }
    }
}
