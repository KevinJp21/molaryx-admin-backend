namespace Domain.Contracts.IServices
{
    public interface IUserLegalAcceptanceService
    {
        Task RecordRegistrationAcceptancesAsync(
            long idUser,
            CancellationToken cancellationToken = default);
    }
}
