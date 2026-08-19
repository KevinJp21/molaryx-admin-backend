using Domain.Models;

namespace Domain.Contracts.IServices
{
    public interface IClinicalHistoryPdfService
    {
        byte[] GenerateClinicalHistoryTemplate(ClinicalHistoryTemplateInformation information);
    }
}
