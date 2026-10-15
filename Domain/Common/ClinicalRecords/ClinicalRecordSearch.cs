using System.Linq.Expressions;
using Domain.Common.Patients;
using Domain.Entities;

namespace Domain.Common.ClinicalRecords
{
    public static class ClinicalRecordSearch
    {
        public static Expression<Func<ClinicalRecord, bool>> MatchesToken(string token)
            => Predicate.For<ClinicalRecord, Patient>(
                clinicalRecord => clinicalRecord.Patient,
                PatientSearch.MatchesToken(token));
    }
}
