using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IClinicalRecordRepository : IBaseRepository<ClinicalRecord, long>{}
}