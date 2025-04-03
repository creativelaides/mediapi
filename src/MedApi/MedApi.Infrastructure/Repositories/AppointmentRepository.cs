using MedApi.Domain;
using MedApi.Application.Interfaces;
using MedApi.Domain.ObjectsValues;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(string specialty)
    {
        return await _dbSet
            .Include(a => a.Doctor)
            .Where(a => a.Specialty == specialty && a.Status == Status.Available)
            .OrderBy(a => a.AppointmentDate)
            .Take(5)
            .ToListAsync();
    }
}