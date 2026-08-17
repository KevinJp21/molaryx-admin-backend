using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PaymentsSpec : BaseSpecification<Payment>
    {
        public PaymentsSpec(
            long idTenant,
            long? idPatient = null,
            long? idAppointment = null,
            long? idPatientTreatment = null)
        {
            Criteria = p => p.IdTenant == idTenant;
            OrderByDescending = p => p.PaidAt;
            AddInclude(p => p.PaymentMethod);

            if (idPatient.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdPatient == idPatient);
            }

            if (idAppointment.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdAppointment == idAppointment);
            }

            if (idPatientTreatment.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdPatientTreatment == idPatientTreatment);
            }
        }
    }
}
