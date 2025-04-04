using MedApi.Application.Interfaces;
using MedApi.Domain;
using MedApi.Domain.ObjectsValues;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(string specialty)
    {
        var now = DateTime.UtcNow;

        return await _dbSet
            .Include(a => a.Doctor)
            .Where(a => a.Specialty == specialty &&
                       a.Status == Status.Available &&
                       a.AppointmentDate > now)
            .OrderBy(a => a.AppointmentDate)
            .Take(5)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdWithTrackingAsync(Guid id)
    {
        return await _dbSet
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}