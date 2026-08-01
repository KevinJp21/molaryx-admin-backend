namespace Infrastructure.Email
{
        public static class EmailTemplateService
    {
        public static string SetParametersToTemplate(
            string template,
            IDictionary<string, string> templateParameters)
        {
            foreach (var parameter in templateParameters)
            {
                var placeholder = $"{{{{{parameter.Key}}}}}";

                template = template.Replace(
                    placeholder,
                    parameter.Value
                );
            }

            return template;
        }
    }
}