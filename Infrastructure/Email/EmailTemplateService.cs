namespace Infrastructure.Email
{
    public static class EmailTemplateService
    {
        private static readonly string TemplatesPath =
            Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Email", "Templates");

        public static string LoadTemplate(string templateName)
        {
            var path = Path.Combine(TemplatesPath, $"{templateName}.html");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"No se encontró la plantilla de correo '{templateName}'.", path
                );
            }

            return File.ReadAllText(path);
        }

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

        public static string RenderTemplate(
            string templateName,
            IDictionary<string, string> templateParameters)
        {
            return SetParametersToTemplate(
                LoadTemplate(templateName),
                templateParameters
            );
        }
    }
}
