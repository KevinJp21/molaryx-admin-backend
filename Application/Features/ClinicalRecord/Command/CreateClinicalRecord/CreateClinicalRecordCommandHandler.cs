using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.ClinicalRecord.Command.CreateClinicalRecord
{
    public class CreateClinicalRecordCommandHandler(
        IClinicalRecordService _clinicalRecordService
    ) : IRequestHandler<CreateClinicalRecordCommand, bool>
    {
        public async Task<bool> Handle(CreateClinicalRecordCommand request, CancellationToken cancellationToken)
        {
            return await _clinicalRecordService.CreateClinicalRecordAsync(request, cancellationToken);
        }
    }
}