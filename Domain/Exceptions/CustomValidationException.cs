namespace Domain.Exceptions
{
    public class CustomValidationException(Dictionary<string, string[]> errors) : ApplicationException(message: "Ha ocurrido o mas errores de validacion")
    {
        public Dictionary<string, string[]> Errors { get; } = errors;
    }
}
