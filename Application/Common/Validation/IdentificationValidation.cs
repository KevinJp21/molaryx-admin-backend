using Application.Common.Regex;
using Domain.Enums;

namespace Application.Common.Validation
{
    public static class IdentificationValidation
    {
        public const int OwnerMinimumAge = 18;

        public static bool IsValidType(short idIdentificationType) =>
            Enum.IsDefined(typeof(IdentificationTypeEnum), idIdentificationType);

        public static bool IsAllowedForOwner(short idIdentificationType) =>
            idIdentificationType is
                (short)IdentificationTypeEnum.CC or
                (short)IdentificationTypeEnum.CE;

        public static bool MatchesType(short idIdentificationType, string? identificationNumber)
        {
            if (string.IsNullOrWhiteSpace(identificationNumber))
                return false;

            var pattern = GetPattern(idIdentificationType);
            return pattern is not null
                && System.Text.RegularExpressions.Regex.IsMatch(identificationNumber, pattern);
        }

        public static bool IsAdult(DateOnly birthDate, DateOnly? today = null)
        {
            var referenceDate = today ?? DateOnly.FromDateTime(DateTime.UtcNow);
            var age = referenceDate.Year - birthDate.Year;

            if (birthDate > referenceDate.AddYears(-age))
                age--;

            return age >= OwnerMinimumAge;
        }

        public static string? GetPattern(short idIdentificationType) =>
            idIdentificationType switch
            {
                (short)IdentificationTypeEnum.NIT => RegexCatalog.NIT,
                (short)IdentificationTypeEnum.CC
                    or (short)IdentificationTypeEnum.CE
                    or (short)IdentificationTypeEnum.TI => RegexCatalog.IDENTIFICATION_NUMBER,
                _ => null
            };
    }
}
