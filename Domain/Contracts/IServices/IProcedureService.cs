using Application.Features.Procedure.Command.CreateProcedure;
using Application.Features.Procedure.Command.UpdateProcedure;

namespace Domain.Contracts.IServices
{
    public interface IProcedureService
    {
        Task<bool> CreateProcedureAsync(CreateProcedureCommand request, CancellationToken cancellationToken);
        Task<bool> UpdateProcedureAsync(UpdateProcedureCommand request, CancellationToken cancellationToken);
        Task<bool> DeleteProcedureAsync(long idProcedure, CancellationToken cancellationToken);
    }
}
