using System.Linq.Expressions;
using Domain.Common.Patients;
using Domain.Entities;

namespace Domain.Common.ClinicalRecords
{
    public static class ClinicalRecordSearch
    {
        public const int MinTokenLength = PatientSearch.MinTokenLength;

        public static string[] GetTokens(string? search)
            => PatientSearch.GetTokens(search);

        public static bool HasValidSearch(string? search)
            => PatientSearch.HasValidSearch(search);

        public static Expression<Func<ClinicalRecord, bool>> MatchesToken(string token)
            => Predicate.For<ClinicalRecord, Patient>(
                clinicalRecord => clinicalRecord.Patient,
                PatientSearch.MatchesToken(token));
    }
}
