using Domain.Models;

namespace Domain.Contracts.IServices
{
    public interface ITemplateBuilderService
    {
        string SetParametersToTemplate(string template, IDictionary<string, string> templateParameters);

        byte[] GenerateClinicalHistoryTemplate(ClinicalHistoryTemplateInformation information);
    }
}
