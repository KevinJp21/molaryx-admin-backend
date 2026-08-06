namespace Domain.Exceptions
{
    public class CustomValidationException(Dictionary<string, string[]> errors)
        : ApplicationException(message: "Se encontraron errores de validación.")
    {
        public Dictionary<string, string[]> Errors { get; } = errors;
    }
}
