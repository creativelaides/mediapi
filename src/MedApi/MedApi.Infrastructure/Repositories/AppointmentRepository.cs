using MedApi.Application.Interfaces;
using MedApi.Domain;
using MedApi.Domain.ObjectsValues;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(Guid doctorId)
    {
        return await _dbSet.Where(a => a.DoctorId == doctorId && a.Status == Status.Available)
                           .ToListAsync();
    }
}
