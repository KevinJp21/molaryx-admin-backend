using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class TreatmentsSpec : BaseSpecification<Treatment>
    {
        public static TreatmentsSpec ById(long idTreatment)
        {
            var spec = new TreatmentsSpec
            {
                Criteria = t => t.IdTreatment == idTreatment && t.DeletedAt == null
            };
            return spec;
        }

        public static TreatmentsSpec ByName(long idTenant, string name)
        {
            var spec = new TreatmentsSpec
            {
                Criteria = t =>
                    t.IdTenant == idTenant
                    && t.DeletedAt == null
                    && t.Name.ToLower() == name.ToLower()
            };
            return spec;
        }

        private TreatmentsSpec()
        {
        }
    }
}
