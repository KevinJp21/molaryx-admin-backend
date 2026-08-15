using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(AppDbContext context)
        : BaseRepository<Appointment, long>(context), IAppointmentRepository
    {
    }
}
