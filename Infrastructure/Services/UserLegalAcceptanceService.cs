using Domain.Constants;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class UserLegalAcceptanceService(
        IUnitOfWork _unitOfWork,
        IHttpContextAccessor _httpContextAccessor
    ) : IUserLegalAcceptanceService
    {
        public async Task RecordRegistrationAcceptancesAsync(
            long idUser,
            CancellationToken cancellationToken = default)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var ipAddress = httpContext.GetClientIpAddress();
            var userAgent = httpContext.GetClientDevice();
            var acceptedAt = DateTime.UtcNow;

            await AddAcceptanceAsync(
                idUser,
                LegalDocumentTypeEnum.TermsAndConditions,
                LegalDocumentVersions.TermsAndConditions,
                acceptedAt,
                ipAddress,
                userAgent,
                cancellationToken);

            await AddAcceptanceAsync(
                idUser,
                LegalDocumentTypeEnum.PrivacyPolicy,
                LegalDocumentVersions.PrivacyPolicy,
                acceptedAt,
                ipAddress,
                userAgent,
                cancellationToken);
        }

        private async Task AddAcceptanceAsync(
            long idUser,
            LegalDocumentTypeEnum documentType,
            string documentVersion,
            DateTime acceptedAt,
            string? ipAddress,
            string? userAgent,
            CancellationToken cancellationToken)
        {
            var acceptance = new UserLegalAcceptance
            {
                IdUser = idUser,
                IdDocumentType = (short)documentType,
                DocumentVersion = documentVersion,
                AcceptedAt = acceptedAt,
                IpAddress = ipAddress,
                UserAgent = userAgent
            };

            await _unitOfWork.UserLegalAcceptanceRepository.AddAsync(
                acceptance,
                cancellationToken);
        }
    }
}
