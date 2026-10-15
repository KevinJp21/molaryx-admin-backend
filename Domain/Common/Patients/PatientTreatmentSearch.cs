using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Common.Patients
{
    public static class PatientTreatmentSearch
    {
        public const int MinTokenLength = Search.MinTokenLength;

        public static string[] GetTokens(string? search)
            => Search.GetTokens(search);

        public static bool HasValidSearch(string? search)
            => Search.HasValidSearch(search);

        public static Expression<Func<PatientTreatment, bool>> MatchesToken(string token)
        {
            var matchesPatient = Predicate.For<PatientTreatment, Patient>(
                patientTreatment => patientTreatment.Patient,
                PatientSearch.MatchesToken(token));

            if (Search.ClassifyToken(token) != SearchTermKind.Name)
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
